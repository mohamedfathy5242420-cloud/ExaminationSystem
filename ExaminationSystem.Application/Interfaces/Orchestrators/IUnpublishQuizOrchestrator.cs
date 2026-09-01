using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;
using ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IUnpublishQuizOrchestrator
{
    Task<Result<PublishQuizViewModel>> UnpublishAsync(
        UnpublishQuizCommand command,
        CancellationToken cancellationToken = default);
}
