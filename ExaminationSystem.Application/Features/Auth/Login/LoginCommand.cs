using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Login.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<Result<LoginViewModel>>;
