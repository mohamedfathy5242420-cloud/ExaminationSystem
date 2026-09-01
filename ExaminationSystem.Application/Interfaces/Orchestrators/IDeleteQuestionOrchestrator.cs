using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IDeleteQuestionOrchestrator
{
    Task<Result<DeleteQuestionViewModel>> DeleteAsync(
        DeleteQuestionCommand command,
        CancellationToken cancellationToken = default);
}
