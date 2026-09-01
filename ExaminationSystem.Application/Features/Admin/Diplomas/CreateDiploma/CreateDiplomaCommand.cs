using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;

public sealed record CreateDiplomaCommand(
    string Title,
    string Description,
    Guid InstructorId) : IRequest<Result<CreateDiplomaViewModel>>;
