using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;

public sealed record GetInstructorDiplomasQuery(
    Guid InstructorId) : IRequest<Result<IReadOnlyList<InstructorDiplomaListItemViewModel>>>;
