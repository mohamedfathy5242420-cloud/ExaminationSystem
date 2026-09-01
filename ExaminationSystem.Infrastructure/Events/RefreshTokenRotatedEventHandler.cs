using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class RefreshTokenRotatedEventHandler : IEventHandler<RefreshTokenRotatedEvent>
{
    private readonly ILogger<RefreshTokenRotatedEventHandler> _logger;

    public RefreshTokenRotatedEventHandler(ILogger<RefreshTokenRotatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        RefreshTokenRotatedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Refresh token rotated for user {UserId} with email {Email}. New refresh token expires at {ExpiresAt}.",
            domainEvent.UserId,
            domainEvent.Email,
            domainEvent.RefreshTokenExpiresAtUtc);

        return Task.CompletedTask;
    }
}
