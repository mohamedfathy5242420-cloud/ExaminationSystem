using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Diplomas.BrowseDiplomas;

public sealed record BrowseDiplomasQuery(
    Guid StudentId) : IRequest<Result<IReadOnlyList<StudentDiplomaListItemViewModel>>>;
