namespace ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.ViewModels;

public sealed record EnrollInDiplomaViewModel(
    Guid EnrollmentId,
    Guid StudentId,
    Guid DiplomaId,
    DateTime EnrolledAt,
    decimal Progress);
