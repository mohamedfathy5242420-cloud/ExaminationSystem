using System.Net;
using System.Net.Mail;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Infrastructure.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExaminationSystem.Infrastructure.Events;

public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly SmtpOptions _smtpOptions;
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(
        IOptions<SmtpOptions> smtpOptions,
        ILogger<UserRegisteredEventHandler> logger)
    {
        _smtpOptions = smtpOptions.Value;
        _logger = logger;
    }

    public async Task HandleAsync(
        UserRegisteredEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_smtpOptions.FromEmail, _smtpOptions.FromName),
            Subject = "Verify your Examination System account",
            Body = BuildBody(domainEvent),
            IsBodyHtml = false
        };

        message.To.Add(new MailAddress(domainEvent.Email, domainEvent.FullName));

        using var smtpClient = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
        {
            EnableSsl = _smtpOptions.EnableSsl,
            Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password)
        };

        await smtpClient.SendMailAsync(message, cancellationToken);

        _logger.LogInformation(
            "OTP email sent to {Email} for user {UserId}.",
            domainEvent.Email,
            domainEvent.UserId);
    }

    private static string BuildBody(UserRegisteredEvent domainEvent)
    {
        return $"""
            Hello {domainEvent.FullName},

            Your Examination System verification code is:
            {domainEvent.OtpCode}

            This code expires in 10 minutes.
            """;
    }
}
