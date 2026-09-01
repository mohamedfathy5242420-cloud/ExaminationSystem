using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class PasswordResetCompletedEventHandler : IEventHandler<PasswordResetCompletedEvent>
{
    private readonly ILogger<PasswordResetCompletedEventHandler> _logger;

    public PasswordResetCompletedEventHandler(ILogger<PasswordResetCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        PasswordResetCompletedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Password reset completed for user {UserId} with email {Email}.",
            domainEvent.UserId,
            domainEvent.Email);

        return Task.CompletedTask;
    }
}
