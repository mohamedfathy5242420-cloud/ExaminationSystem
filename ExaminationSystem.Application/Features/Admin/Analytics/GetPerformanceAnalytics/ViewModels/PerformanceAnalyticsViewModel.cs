namespace ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics.ViewModels;

public sealed record PerformanceAnalyticsViewModel(
    int TotalAttempts,
    int CompletedAttempts,
    int PassedAttempts,
    decimal OverallPassRate,
    IReadOnlyList<QuizPassRateViewModel> PassRateByQuiz,
    IReadOnlyList<DiplomaAverageScoreViewModel> AverageScoreByDiploma,
    IReadOnlyList<AttemptsOverTimeViewModel> AttemptsOverTime,
    IReadOnlyList<FailedQuestionViewModel> MostFailedQuestions);
