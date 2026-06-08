using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.Register;

public sealed record UserRegisteredEvent(
    Guid UserId,
    string Email,
    string FullName,
    string OtpCode) : IEvent;
