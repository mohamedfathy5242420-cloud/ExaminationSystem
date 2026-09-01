using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.ForgotPassword;

public sealed record PasswordResetRequestedEvent(
    Guid UserId,
    string Email,
    string FullName,
    string ResetCode) : IEvent;
