namespace ExaminationSystem.Application.Features.Auth.ResetPassword.Requests;

public sealed record ResetPasswordRequest(
    string Email,
    string OtpCode,
    string NewPassword);
