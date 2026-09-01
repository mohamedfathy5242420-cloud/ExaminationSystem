using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.RefreshToken;
using ExaminationSystem.Application.Features.Auth.RefreshToken.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class RefreshTokenOrchestrator : IRefreshTokenOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenBuilder _jwtTokenBuilder;
    private readonly IEventDispatcher _eventDispatcher;

    public RefreshTokenOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        JwtTokenBuilder jwtTokenBuilder,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _jwtTokenBuilder = jwtTokenBuilder;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<RefreshTokenViewModel>> RefreshAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var oldRefreshToken = _unitOfWork.Repository<RefreshToken>()
            .Query()
            .FirstOrDefault(x => x.Token == command.RefreshToken.Trim());

        if (oldRefreshToken is null || oldRefreshToken.Revoked)
        {
            return Result<RefreshTokenViewModel>.Failure("Invalid refresh token.");
        }

        if (oldRefreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            return Result<RefreshTokenViewModel>.Failure("Refresh token has expired.");
        }

        var user = await _userManager.FindByIdAsync(oldRefreshToken.UserId.ToString());
        if (user is null)
        {
            return Result<RefreshTokenViewModel>.Failure("Invalid refresh token.");
        }

        if (user.Status != UserStatus.Active || !user.EmailConfirmed)
        {
            return Result<RefreshTokenViewModel>.Failure("Account is not active.");
        }

        var tokenResult = _jwtTokenBuilder.Build(user);

        oldRefreshToken.Revoked = true;
        _unitOfWork.Repository<RefreshToken>().Update(oldRefreshToken);

        var newRefreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokenResult.RefreshToken,
            ExpiryDate = tokenResult.RefreshTokenExpiresAtUtc,
            Revoked = false
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new RefreshTokenRotatedEvent(user.Id, user.Email!, tokenResult.RefreshTokenExpiresAtUtc),
            cancellationToken);

        var viewModel = new RefreshTokenViewModel(
            tokenResult.AccessToken,
            tokenResult.AccessTokenExpiresAtUtc,
            tokenResult.RefreshToken,
            tokenResult.RefreshTokenExpiresAtUtc);

        return Result<RefreshTokenViewModel>.Success(viewModel);
    }
}
