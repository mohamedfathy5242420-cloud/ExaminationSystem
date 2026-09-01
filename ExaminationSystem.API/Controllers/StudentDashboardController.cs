using System.IdentityModel.Tokens.Jwt;
using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Student.Dashboard.GetStudentDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.StudentOnly)]
[Route("api/student/dashboard")]
public class StudentDashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentDashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var studentIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        if (!Guid.TryParse(studentIdClaim, out var studentId) || userType != "Student")
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetStudentDashboardQuery(studentId),
            cancellationToken);

        return Ok(result.Value);
    }
}
