namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

public sealed record DiplomaAverageScoreViewModel(
    Guid DiplomaId,
    string DiplomaTitle,
    decimal AverageScore);
