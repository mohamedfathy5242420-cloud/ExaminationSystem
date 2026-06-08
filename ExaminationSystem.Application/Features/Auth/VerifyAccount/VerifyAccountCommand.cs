using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.VerifyAccount.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.VerifyAccount;

public sealed record VerifyAccountCommand(
    string Email,
    string OtpCode) : IRequest<Result<VerifyAccountViewModel>>;
