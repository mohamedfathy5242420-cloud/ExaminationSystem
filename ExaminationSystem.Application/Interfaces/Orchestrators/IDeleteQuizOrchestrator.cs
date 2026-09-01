using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.DeleteQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IDeleteQuizOrchestrator
{
    Task<Result<DeleteQuizViewModel>> DeleteAsync(
        DeleteQuizCommand command,
        CancellationToken cancellationToken = default);
}
