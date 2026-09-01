using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application.Features.Admin.Analytics.GetPerformanceAnalytics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers;

[ApiController]
[Authorize(Policy = UserTypePolicies.AdminOnly)]
[Route("api/admin/analytics")]
public class AdminAnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminAnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("performance")]
    public async Task<IActionResult> GetPerformanceAnalytics(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetPerformanceAnalyticsQuery(),
            cancellationToken);

        return Ok(result.Value);
    }
}
