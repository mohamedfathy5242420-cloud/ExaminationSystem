using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.VerifyAccount;

public sealed record AccountVerifiedEvent(
    Guid UserId,
    string Email,
    string FullName) : IEvent;
