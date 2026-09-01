using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;
using ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IPublishQuizOrchestrator
{
    Task<Result<PublishQuizViewModel>> PublishAsync(
        PublishQuizCommand command,
        CancellationToken cancellationToken = default);
}
