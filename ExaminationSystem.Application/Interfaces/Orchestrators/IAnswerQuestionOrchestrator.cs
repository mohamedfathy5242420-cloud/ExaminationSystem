using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IAnswerQuestionOrchestrator
{
    Task<Result<AnswerQuestionViewModel>> AnswerAsync(
        AnswerQuestionCommand command,
        CancellationToken cancellationToken = default);
}
