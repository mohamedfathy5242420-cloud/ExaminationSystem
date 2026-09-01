using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;

public sealed record GetStudentAttemptsQuery(
    Guid? StudentId,
    Guid? QuizId,
    string? Status) : IRequest<Result<IReadOnlyList<AdminStudentAttemptListItemViewModel>>>;
