using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.GetDiplomaQuizzes;

public sealed class GetDiplomaQuizzesForStudentQueryHandler
    : IRequestHandler<GetDiplomaQuizzesForStudentQuery, Result<IReadOnlyList<StudentDiplomaQuizViewModel>>>
{
    private readonly IGetDiplomaQuizzesForStudentOrchestrator _orchestrator;

    public GetDiplomaQuizzesForStudentQueryHandler(
        IGetDiplomaQuizzesForStudentOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<IReadOnlyList<StudentDiplomaQuizViewModel>>> Handle(
        GetDiplomaQuizzesForStudentQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
