using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;

public sealed class GetInstructorDiplomaQuizzesQueryHandler
    : IRequestHandler<GetInstructorDiplomaQuizzesQuery, Result<IReadOnlyList<InstructorQuizListItemViewModel>>>
{
    private readonly IGetInstructorDiplomaQuizzesOrchestrator _orchestrator;

    public GetInstructorDiplomaQuizzesQueryHandler(IGetInstructorDiplomaQuizzesOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<IReadOnlyList<InstructorQuizListItemViewModel>>> Handle(
        GetInstructorDiplomaQuizzesQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
