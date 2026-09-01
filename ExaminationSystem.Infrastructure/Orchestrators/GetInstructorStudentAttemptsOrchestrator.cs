using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Domain.Entities.Learning;
using Microsoft.AspNetCore.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetInstructorStudentAttemptsOrchestrator : IGetInstructorStudentAttemptsOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetInstructorStudentAttemptsOrchestrator(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public Task<Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>> GetAsync(
        GetInstructorStudentAttemptsQuery query,
        CancellationToken cancellationToken = default)
    {
        var diplomasQuery = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => x.InstructorId == query.InstructorId);

        if (query.DiplomaId.HasValue)
        {
            diplomasQuery = diplomasQuery.Where(x => x.Id == query.DiplomaId.Value);
        }

        var diplomas = diplomasQuery.ToList();
        var diplomaIds = diplomas.Select(x => x.Id).ToList();

        var quizzesQuery = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => diplomaIds.Contains(x.DiplomaId));

        if (query.QuizId.HasValue)
        {
            quizzesQuery = quizzesQuery.Where(x => x.Id == query.QuizId.Value);
        }

        var quizzes = quizzesQuery.ToList();
        var quizIds = quizzes.Select(x => x.Id).ToList();

        var attemptsQuery = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => quizIds.Contains(x.QuizId));

        if (!string.IsNullOrWhiteSpace(query.Status)
            && Enum.TryParse<AttemptStatus>(query.Status, true, out var status))
        {
            attemptsQuery = attemptsQuery.Where(x => x.Status == status);
        }

        var attempts = attemptsQuery
            .OrderByDescending(x => x.StartTime)
            .ToList();

        var studentIds = attempts.Select(x => x.StudentId).Distinct().ToList();
        var students = _userManager.Users
            .Where(x => studentIds.Contains(x.Id))
            .ToList();

        var viewModels = attempts
            .Select(attempt =>
            {
                var quiz = quizzes.First(x => x.Id == attempt.QuizId);
                var diploma = diplomas.First(x => x.Id == quiz.DiplomaId);
                var student = students.FirstOrDefault(x => x.Id == attempt.StudentId);

                return new InstructorStudentAttemptListItemViewModel(
                    attempt.Id,
                    attempt.StudentId,
                    student?.FullName ?? "Deleted student",
                    student?.Email ?? string.Empty,
                    quiz.Id,
                    quiz.Title,
                    diploma.Id,
                    diploma.Title,
                    attempt.Status.ToString(),
                    attempt.Score,
                    quiz.PassScore,
                    attempt.IsPassed,
                    attempt.StartTime,
                    attempt.EndTime);
            })
            .ToList();

        return Task.FromResult(
            Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>.Success(viewModels));
    }
}
