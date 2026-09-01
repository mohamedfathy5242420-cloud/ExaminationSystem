namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.Requests;

public sealed record CreateDiplomaRequest(
    string Title,
    string Description,
    Guid InstructorId);
