namespace ExaminationSystem.Application.Features.Auth.RefreshToken.ViewModels;

public sealed record RefreshTokenViewModel(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);
