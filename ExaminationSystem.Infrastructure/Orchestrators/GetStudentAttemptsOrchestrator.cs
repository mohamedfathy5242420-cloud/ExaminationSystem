using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.AspNetCore.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetStudentAttemptsOrchestrator : IGetStudentAttemptsOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetStudentAttemptsOrchestrator(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public Task<Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>> GetAsync(
        GetStudentAttemptsQuery query,
        CancellationToken cancellationToken = default)
    {
        var attemptsQuery = _unitOfWork.Repository<QuizAttempt>().Query();

        if (query.StudentId.HasValue)
        {
            attemptsQuery = attemptsQuery.Where(x => x.StudentId == query.StudentId.Value);
        }

        if (query.QuizId.HasValue)
        {
            attemptsQuery = attemptsQuery.Where(x => x.QuizId == query.QuizId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Status)
            && Enum.TryParse<AttemptStatus>(query.Status, true, out var status))
        {
            attemptsQuery = attemptsQuery.Where(x => x.Status == status);
        }

        var attempts = attemptsQuery
            .OrderByDescending(x => x.StartTime)
            .ToList();

        var quizIds = attempts.Select(x => x.QuizId).Distinct().ToList();
        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => quizIds.Contains(x.Id))
            .ToList();

        var diplomaIds = quizzes.Select(x => x.DiplomaId).Distinct().ToList();
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => diplomaIds.Contains(x.Id))
            .ToList();

        var users = _userManager.Users
            .Where(x => attempts.Select(attempt => attempt.StudentId).Contains(x.Id))
            .ToList();

        var viewModels = attempts
            .Select(attempt =>
            {
                var quiz = quizzes.FirstOrDefault(x => x.Id == attempt.QuizId);
                var diploma = quiz is null
                    ? null
                    : diplomas.FirstOrDefault(x => x.Id == quiz.DiplomaId);
                var student = users.FirstOrDefault(x => x.Id == attempt.StudentId);

                return new AdminStudentAttemptListItemViewModel(
                    attempt.Id,
                    attempt.StudentId,
                    student?.FullName ?? "Deleted student",
                    student?.Email ?? string.Empty,
                    attempt.QuizId,
                    quiz?.Title ?? "Deleted quiz",
                    quiz?.DiplomaId ?? Guid.Empty,
                    diploma?.Title ?? "Deleted diploma",
                    attempt.Status.ToString(),
                    attempt.Score,
                    quiz?.PassScore ?? 0,
                    attempt.IsPassed,
                    attempt.StartTime,
                    attempt.EndTime);
            })
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>.Success(viewModels));
    }
}
