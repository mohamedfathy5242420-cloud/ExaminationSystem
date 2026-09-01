using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

namespace ExaminationSystem.Application.Interfaces.Orchestrators;

public interface IGetPerformanceAnalyticsOrchestrator
{
    Task<Result<PerformanceAnalyticsViewModel>> GetAsync(
        GetPerformanceAnalyticsQuery query,
        CancellationToken cancellationToken = default);
}
