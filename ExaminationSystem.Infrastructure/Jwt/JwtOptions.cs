namespace ExaminationSystem.Infrastructure.Jwt;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public string Secret { get; set; } = default!;

    public int AccessTokenExpirationMinutes { get; set; } = 60;

    public int RefreshTokenExpirationDays { get; set; } = 7;
}
