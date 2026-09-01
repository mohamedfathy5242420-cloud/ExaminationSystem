using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;

public sealed record UpdateDiplomaCommand(
    Guid Id,
    string Title,
    string Description,
    Guid InstructorId) : IRequest<Result<UpdateDiplomaViewModel>>;
