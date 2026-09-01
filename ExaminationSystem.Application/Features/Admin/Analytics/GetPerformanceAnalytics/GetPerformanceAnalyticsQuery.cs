using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;

public sealed record GetPerformanceAnalyticsQuery : IRequest<Result<PerformanceAnalyticsViewModel>>;
