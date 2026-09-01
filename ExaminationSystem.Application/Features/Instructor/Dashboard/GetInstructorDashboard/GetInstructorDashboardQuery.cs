using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;

public sealed record GetInstructorDashboardQuery(
    Guid InstructorId) : IRequest<Result<InstructorDashboardViewModel>>;
