using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;
using ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface ICreateQuestionOrchestrator
{
    Task<Result<CreateQuestionViewModel>> CreateAsync(
        CreateQuestionCommand command,
        CancellationToken cancellationToken = default);
}
