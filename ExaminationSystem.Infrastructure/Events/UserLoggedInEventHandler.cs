using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.Login;
using Microsoft.Extensions.Logging;

namespace ExaminationSystem.Infrastructure.Events;

public class UserLoggedInEventHandler : IEventHandler<UserLoggedInEvent>
{
    private readonly ILogger<UserLoggedInEventHandler> _logger;

    public UserLoggedInEventHandler(ILogger<UserLoggedInEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(
        UserLoggedInEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "User logged in: {UserId}, {Email}, {UserType}.",
            domainEvent.UserId,
            domainEvent.Email,
            domainEvent.UserType);

        return Task.CompletedTask;
    }
}
