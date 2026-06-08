namespace ExaminationSystem.Application.Features.Auth.VerifyAccount.Requests;

public sealed record VerifyAccountRequest(
    string Email,
    string OtpCode);
