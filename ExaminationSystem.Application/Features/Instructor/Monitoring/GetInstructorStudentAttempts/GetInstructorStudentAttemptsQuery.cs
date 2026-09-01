using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;

public sealed record GetInstructorStudentAttemptsQuery(
    Guid InstructorId,
    Guid? DiplomaId,
    Guid? QuizId,
    string? Status) : IRequest<Result<IReadOnlyList<InstructorStudentAttemptListItemViewModel>>>;
