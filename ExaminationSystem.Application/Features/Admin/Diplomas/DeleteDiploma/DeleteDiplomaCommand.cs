using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;

public sealed record DeleteDiplomaCommand(
    Guid Id) : IRequest<Result<DeleteDiplomaViewModel>>;
