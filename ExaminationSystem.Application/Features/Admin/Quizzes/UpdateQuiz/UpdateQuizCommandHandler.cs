using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UpdateQuiz;

public sealed class UpdateQuizCommandHandler
    : IRequestHandler<UpdateQuizCommand, Result<UpdateQuizViewModel>>
{
    private readonly IUpdateQuizOrchestrator _updateQuizOrchestrator;

    public UpdateQuizCommandHandler(IUpdateQuizOrchestrator updateQuizOrchestrator)
    {
        _updateQuizOrchestrator = updateQuizOrchestrator;
    }

    public Task<Result<UpdateQuizViewModel>> Handle(
        UpdateQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _updateQuizOrchestrator.UpdateAsync(command, cancellationToken);
    }
}
