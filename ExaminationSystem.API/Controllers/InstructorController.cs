using System.IdentityModel.Tokens.Jwt;
using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Instructor.Dashboard.GetInstructorDashboard;
using ExaminationSystem.Application.Features.Instructor.Diplomas.GetInstructorDiplomas;
using ExaminationSystem.Application.Features.Instructor.Monitoring.GetInstructorStudentAttempts;
using ExaminationSystem.Application.Features.Instructor.Quizzes.GetInstructorDiplomaQuizzes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.InstructorOnly)]
[Route("api/instructor")]
public class InstructorController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstructorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var instructorId = GetInstructorId();
        if (instructorId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetInstructorDashboardQuery(instructorId.Value),
            cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("diplomas")]
    public async Task<IActionResult> GetDiplomas(CancellationToken cancellationToken)
    {
        var instructorId = GetInstructorId();
        if (instructorId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetInstructorDiplomasQuery(instructorId.Value),
            cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("diplomas/{diplomaId:guid}/quizzes")]
    public async Task<IActionResult> GetDiplomaQuizzes(
        Guid diplomaId,
        CancellationToken cancellationToken)
    {
        var instructorId = GetInstructorId();
        if (instructorId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetInstructorDiplomaQuizzesQuery(instructorId.Value, diplomaId),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return Ok(result.Value);
    }

    [HttpGet("attempts")]
    public async Task<IActionResult> GetStudentAttempts(
        [FromQuery] Guid? diplomaId,
        [FromQuery] Guid? quizId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var instructorId = GetInstructorId();
        if (instructorId is null)
        {
            return Forbid();
        }

        var result = await _mediator.Send(
            new GetInstructorStudentAttemptsQuery(instructorId.Value, diplomaId, quizId, status),
            cancellationToken);

        return Ok(result.Value);
    }

    private Guid? GetInstructorId()
    {
        var instructorIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var userType = User.FindFirst("user_type")?.Value;

        return Guid.TryParse(instructorIdClaim, out var instructorId) && userType == "Instructor"
            ? instructorId
            : null;
    }
}
