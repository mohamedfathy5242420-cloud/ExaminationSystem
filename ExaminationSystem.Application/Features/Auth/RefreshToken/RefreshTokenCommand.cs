using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Features.Auth.RefreshToken.ViewModels;
using MediatR;

namespace ExaminationSystem.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken) : IRequest<Result<RefreshTokenViewModel>>;
