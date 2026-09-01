namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;

public sealed record StudentDashboardDiplomaViewModel(
    Guid DiplomaId,
    string Title,
    decimal Progress,
    DateTime EnrolledAt);
