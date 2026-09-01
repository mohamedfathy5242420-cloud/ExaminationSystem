using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenRotatedEvent(
    Guid UserId,
    string Email,
    DateTime RefreshTokenExpiresAtUtc) : IEvent;
