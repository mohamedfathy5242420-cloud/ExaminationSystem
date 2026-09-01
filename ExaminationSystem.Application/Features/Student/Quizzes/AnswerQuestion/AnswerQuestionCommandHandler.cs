using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;

public sealed class AnswerQuestionCommandHandler
    : IRequestHandler<AnswerQuestionCommand, Result<AnswerQuestionViewModel>>
{
    private readonly IAnswerQuestionOrchestrator _answerQuestionOrchestrator;

    public AnswerQuestionCommandHandler(IAnswerQuestionOrchestrator answerQuestionOrchestrator)
    {
        _answerQuestionOrchestrator = answerQuestionOrchestrator;
    }

    public Task<Result<AnswerQuestionViewModel>> Handle(
        AnswerQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return _answerQuestionOrchestrator.AnswerAsync(command, cancellationToken);
    }
}
