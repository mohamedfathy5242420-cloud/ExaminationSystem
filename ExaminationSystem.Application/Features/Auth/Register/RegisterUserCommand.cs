using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.Register.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.Register;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    string UserType) : IRequest<Result<RegisterUserViewModel>>;
