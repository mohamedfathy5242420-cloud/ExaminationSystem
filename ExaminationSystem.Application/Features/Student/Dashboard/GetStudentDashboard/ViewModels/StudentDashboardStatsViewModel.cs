namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;

public sealed record StudentDashboardStatsViewModel(
    int EnrolledDiplomasCount,
    int AttemptsCount,
    int PassedAttemptsCount,
    decimal PassRate,
    decimal AverageScore);
