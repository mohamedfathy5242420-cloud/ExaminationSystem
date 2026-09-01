namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;

public sealed record StudentDashboardViewModel(
    IReadOnlyList<StudentDashboardDiplomaViewModel> Diplomas,
    IReadOnlyList<StudentDashboardAttemptViewModel> LatestAttempts,
    StudentDashboardStatsViewModel Stats);
