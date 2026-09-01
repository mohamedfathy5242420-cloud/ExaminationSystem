using System.Net;
using System.Net.Mail;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Infrastructure.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExaminationSystem.Infrastructure.Events;

public class PasswordResetRequestedEventHandler : IEventHandler<PasswordResetRequestedEvent>
{
    private readonly SmtpOptions _smtpOptions;
    private readonly ILogger<PasswordResetRequestedEventHandler> _logger;

    public PasswordResetRequestedEventHandler(
        IOptions<SmtpOptions> smtpOptions,
        ILogger<PasswordResetRequestedEventHandler> logger)
    {
        _smtpOptions = smtpOptions.Value;
        _logger = logger;
    }

    public async Task HandleAsync(
        PasswordResetRequestedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_smtpOptions.FromEmail, _smtpOptions.FromName),
            Subject = "Reset your Examination System password",
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
            "Password reset code sent to {Email} for user {UserId}.",
            domainEvent.Email,
            domainEvent.UserId);
    }

    private static string BuildBody(PasswordResetRequestedEvent domainEvent)
    {
        return $"""
            Hello {domainEvent.FullName},

            Your password reset code is:
            {domainEvent.ResetCode}

            This code expires in 10 minutes.
            """;
    }
}
