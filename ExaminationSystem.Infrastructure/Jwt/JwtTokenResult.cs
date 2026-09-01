namespace ExaminationSystem.Infrastructure.Jwt;

public sealed record JwtTokenResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);
