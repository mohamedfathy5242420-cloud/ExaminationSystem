using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttemptDetails;
using ExaminationSystem.Application.Features.Admin.Monitoring.GetStudentAttempts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/admin/monitoring")]
public class AdminMonitoringController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminMonitoringController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("attempts")]
    public async Task<IActionResult> GetStudentAttempts(
        [FromQuery] Guid? studentId,
        [FromQuery] Guid? quizId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetStudentAttemptsQuery(studentId, quizId, status),
            cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("attempts/{attemptId:guid}")]
    public async Task<IActionResult> GetStudentAttemptDetails(
        Guid attemptId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetStudentAttemptDetailsQuery(attemptId),
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
}
