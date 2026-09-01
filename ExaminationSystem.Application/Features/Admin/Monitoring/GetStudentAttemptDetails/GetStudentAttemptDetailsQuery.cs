using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;

public sealed record GetStudentAttemptDetailsQuery(
    Guid AttemptId) : IRequest<Result<AdminStudentAttemptDetailsViewModel>>;
