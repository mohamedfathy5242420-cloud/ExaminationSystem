using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.UpdateQuestion.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IUpdateQuestionOrchestrator
{
    Task<Result<UpdateQuestionViewModel>> UpdateAsync(
        UpdateQuestionCommand command,
        CancellationToken cancellationToken = default);
}
