using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;

public sealed class GetPerformanceAnalyticsQueryHandler
    : IRequestHandler<GetPerformanceAnalyticsQuery, Result<PerformanceAnalyticsViewModel>>
{
    private readonly IGetPerformanceAnalyticsOrchestrator _orchestrator;

    public GetPerformanceAnalyticsQueryHandler(IGetPerformanceAnalyticsOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public Task<Result<PerformanceAnalyticsViewModel>> Handle(
        GetPerformanceAnalyticsQuery query,
        CancellationToken cancellationToken)
    {
        return _orchestrator.GetAsync(query, cancellationToken);
    }
}
