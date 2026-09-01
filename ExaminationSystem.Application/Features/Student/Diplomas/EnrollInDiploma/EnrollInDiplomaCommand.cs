using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;

public sealed record EnrollInDiplomaCommand(
    Guid StudentId,
    Guid DiplomaId) : IRequest<Result<EnrollInDiplomaViewModel>>;
