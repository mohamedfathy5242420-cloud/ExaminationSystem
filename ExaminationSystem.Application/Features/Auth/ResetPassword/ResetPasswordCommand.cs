using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ResetPassword.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string OtpCode,
    string NewPassword) : IRequest<Result<ResetPasswordViewModel>>;
