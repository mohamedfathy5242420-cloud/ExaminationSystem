using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class AccountVerifiedEventHandler : IEventHandler<AccountVerifiedEvent>
{
    private readonly ILogger<AccountVerifiedEventHandler> _logger;

    public AccountVerifiedEventHandler(ILogger<AccountVerifiedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        AccountVerifiedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Account verified for user {UserId} with email {Email}.",
            domainEvent.UserId,
            domainEvent.Email);

        return Task.CompletedTask;
    }
}
