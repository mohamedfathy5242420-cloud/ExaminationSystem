namespace ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard.ViewModels;

public sealed record InstructorDashboardViewModel(
    int DiplomasCount,
    int PublishedDiplomasCount,
    int QuizzesCount,
    int PublishedQuizzesCount,
    int StudentAttemptsCount,
    decimal AverageScore);
