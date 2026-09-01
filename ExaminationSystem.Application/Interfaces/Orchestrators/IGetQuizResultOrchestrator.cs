using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult;
using ExaminationSystem.Application.Features.Student.Quizzes.GetQuizResult.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetQuizResultOrchestrator
{
    Task<Result<QuizResultViewModel>> GetAsync(
        GetQuizResultQuery query,
        CancellationToken cancellationToken = default);
}
