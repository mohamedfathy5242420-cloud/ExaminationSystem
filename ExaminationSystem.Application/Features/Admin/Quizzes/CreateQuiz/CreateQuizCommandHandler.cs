using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;

public sealed class CreateQuizCommandHandler
    : IRequestHandler<CreateQuizCommand, Result<CreateQuizViewModel>>
{
    private readonly ICreateQuizOrchestrator _createQuizOrchestrator;

    public CreateQuizCommandHandler(ICreateQuizOrchestrator createQuizOrchestrator)
    {
        _createQuizOrchestrator = createQuizOrchestrator;
    }

    public Task<Result<CreateQuizViewModel>> Handle(
        CreateQuizCommand command,
        CancellationToken cancellationToken)
    {
        return _createQuizOrchestrator.CreateAsync(command, cancellationToken);
    }
}
