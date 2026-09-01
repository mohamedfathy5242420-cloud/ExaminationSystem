namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

public sealed record AttemptsOverTimeViewModel(
    DateOnly Date,
    int AttemptsCount);
