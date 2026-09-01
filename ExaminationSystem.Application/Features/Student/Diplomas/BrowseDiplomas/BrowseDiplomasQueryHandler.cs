using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;

public sealed class BrowseDiplomasQueryHandler
    : IRequestHandler<BrowseDiplomasQuery, Result<IReadOnlyList<StudentDiplomaListItemViewModel>>>
{
    private readonly IBrowseDiplomasOrchestrator _browseDiplomasOrchestrator;

    public BrowseDiplomasQueryHandler(IBrowseDiplomasOrchestrator browseDiplomasOrchestrator)
    {
        _browseDiplomasOrchestrator = browseDiplomasOrchestrator;
    }

    public Task<Result<IReadOnlyList<StudentDiplomaListItemViewModel>>> Handle(
        BrowseDiplomasQuery query,
        CancellationToken cancellationToken)
    {
        return _browseDiplomasOrchestrator.GetAsync(query, cancellationToken);
    }
}
