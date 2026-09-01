namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.Requests;

public sealed record UpdateDiplomaRequest(
    string Title,
    string Description,
    Guid InstructorId);
