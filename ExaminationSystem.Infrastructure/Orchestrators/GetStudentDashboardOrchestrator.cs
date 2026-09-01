using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetStudentDashboardOrchestrator : IGetStudentDashboardOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentDashboardOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<StudentDashboardViewModel>> GetAsync(
        GetStudentDashboardQuery query,
        CancellationToken cancellationToken = default)
    {
        var enrollments = _unitOfWork.Repository<Enrollment>()
            .Query()
            .Where(x => x.StudentId == query.StudentId)
            .OrderByDescending(x => x.EnrolledAt)
            .ToList();

        var diplomaIds = enrollments.Select(x => x.DiplomaId).ToList();
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => diplomaIds.Contains(x.Id))
            .ToList();

        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => x.StudentId == query.StudentId)
            .OrderByDescending(x => x.StartTime)
            .ToList();

        var quizIds = attempts.Select(x => x.QuizId).Distinct().ToList();
        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => quizIds.Contains(x.Id))
            .ToList();

        var diplomaViewModels = enrollments
            .Select(enrollment =>
            {
                var diploma = diplomas.FirstOrDefault(x => x.Id == enrollment.DiplomaId);

                return new StudentDashboardDiplomaViewModel(
                    enrollment.DiplomaId,
                    diploma?.Title ?? "Deleted diploma",
                    enrollment.Progress,
                    enrollment.EnrolledAt);
            })
            .ToList();

        var latestAttempts = attempts
            .Take(5)
            .Select(attempt =>
            {
                var quiz = quizzes.FirstOrDefault(x => x.Id == attempt.QuizId);

                return new StudentDashboardAttemptViewModel(
                    attempt.Id,
                    attempt.QuizId,
                    quiz?.Title ?? "Deleted quiz",
                    attempt.Score,
                    quiz?.PassScore ?? 0,
                    attempt.IsPassed,
                    attempt.Status.ToString(),
                    attempt.StartTime,
                    attempt.EndTime);
            })
            .ToList();

        var closedAttempts = attempts
            .Where(x => x.Status != AttemptStatus.InProgress)
            .ToList();

        var stats = new StudentDashboardStatsViewModel(
            enrollments.Count,
            attempts.Count,
            closedAttempts.Count(x => x.IsPassed),
            closedAttempts.Count == 0
                ? 0
                : Math.Round((decimal)closedAttempts.Count(x => x.IsPassed) / closedAttempts.Count * 100, 2),
            closedAttempts.Count == 0
                ? 0
                : Math.Round((decimal)closedAttempts.Average(x => x.Score), 2));

        var viewModel = new StudentDashboardViewModel(
            diplomaViewModels,
            latestAttempts,
            stats);

        return Task.FromResult(Result<StudentDashboardViewModel>.Success(viewModel));
    }
}
