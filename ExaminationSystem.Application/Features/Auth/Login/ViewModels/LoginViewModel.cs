namespace ExaminationSystem.Application.Features.Auth.Login.ViewModels;

public sealed record LoginViewModel(
    Guid UserId,
    string FullName,
    string Email,
    string UserType,
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);
