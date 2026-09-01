using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.GetDiplomas;

public sealed record GetDiplomasQuery : IRequest<Result<IReadOnlyList<DiplomaListItemViewModel>>>;
