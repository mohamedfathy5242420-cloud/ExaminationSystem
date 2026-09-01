namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

public sealed record QuizPassRateViewModel(
    Guid QuizId,
    string QuizTitle,
    int AttemptsCount,
    int PassedAttemptsCount,
    decimal PassRate);
