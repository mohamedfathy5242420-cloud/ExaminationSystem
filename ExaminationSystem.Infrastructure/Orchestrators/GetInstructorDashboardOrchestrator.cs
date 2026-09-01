using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Learning;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class GetInstructorDashboardOrchestrator : IGetInstructorDashboardOrchestrator
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInstructorDashboardOrchestrator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Result<InstructorDashboardViewModel>> GetAsync(
        GetInstructorDashboardQuery query,
        CancellationToken cancellationToken = default)
    {
        var diplomas = _unitOfWork.Repository<Diploma>()
            .Query()
            .Where(x => x.InstructorId == query.InstructorId)
            .ToList();

        var diplomaIds = diplomas.Select(x => x.Id).ToList();
        var quizzes = _unitOfWork.Repository<QuizEntity>()
            .Query()
            .Where(x => diplomaIds.Contains(x.DiplomaId))
            .ToList();

        var quizIds = quizzes.Select(x => x.Id).ToList();
        var attempts = _unitOfWork.Repository<QuizAttempt>()
            .Query()
            .Where(x => quizIds.Contains(x.QuizId))
            .ToList();

        var closedAttempts = attempts
            .Where(x => x.Status != AttemptStatus.InProgress)
            .ToList();

        var viewModel = new InstructorDashboardViewModel(
            diplomas.Count,
            diplomas.Count(x => x.IsPublished),
            quizzes.Count,
            quizzes.Count(x => x.IsPublished),
            attempts.Count,
            closedAttempts.Count == 0
                ? 0
                : Math.Round((decimal)closedAttempts.Average(x => x.Score), 2));

        return Task.FromResult(Result<InstructorDashboardViewModel>.Success(viewModel));
    }
}
