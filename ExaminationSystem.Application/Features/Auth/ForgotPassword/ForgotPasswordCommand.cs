using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.ForgotPassword.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email) : IRequest<Result<ForgotPasswordViewModel>>;
