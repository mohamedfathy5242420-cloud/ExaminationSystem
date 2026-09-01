using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.Login;
using ExaminationSystem.Application.Features.Auth.Login.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using ExaminationSystem.Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class LoginOrchestrator : ILoginOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenBuilder _jwtTokenBuilder;
    private readonly IEventDispatcher _eventDispatcher;

    public LoginOrchestrator(
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

    public async Task<Result<LoginViewModel>> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email.Trim();
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<LoginViewModel>.Failure("Invalid email or password.");
        }

        if (user.Status == UserStatus.Pending || !user.EmailConfirmed)
        {
            return Result<LoginViewModel>.Failure("Account is not verified.");
        }

        if (user.Status == UserStatus.Locked)
        {
            return Result<LoginViewModel>.Failure("Account is locked.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!isPasswordValid)
        {
            return Result<LoginViewModel>.Failure("Invalid email or password.");
        }

        var tokenResult = _jwtTokenBuilder.Build(user);

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokenResult.RefreshToken,
            ExpiryDate = tokenResult.RefreshTokenExpiresAtUtc,
            Revoked = false
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userType = user.GetType().Name;

        await _eventDispatcher.DispatchAsync(
            new UserLoggedInEvent(user.Id, user.Email!, user.FullName, userType),
            cancellationToken);

        var viewModel = new LoginViewModel(
            user.Id,
            user.FullName,
            user.Email!,
            userType,
            tokenResult.AccessToken,
            tokenResult.AccessTokenExpiresAtUtc,
            tokenResult.RefreshToken,
            tokenResult.RefreshTokenExpiresAtUtc);

        return Result<LoginViewModel>.Success(viewModel);
    }
}
