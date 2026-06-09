using ExaminationSystem.Application.Common.Events;

namespace ExaminationSystem.Application.Features.Auth.Login;

public sealed record UserLoggedInEvent(
    Guid UserId,
    string Email,
    string FullName,
    string UserType) : IEvent;
