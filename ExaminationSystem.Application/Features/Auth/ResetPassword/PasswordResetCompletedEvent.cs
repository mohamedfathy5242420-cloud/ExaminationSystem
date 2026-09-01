using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.ResetPassword;

public sealed record PasswordResetCompletedEvent(
    Guid UserId,
    string Email,
    string FullName) : IEvent;
