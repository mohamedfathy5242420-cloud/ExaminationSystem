using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;

public sealed record GetStudentDashboardQuery(
    Guid StudentId) : IRequest<Result<StudentDashboardViewModel>>;
