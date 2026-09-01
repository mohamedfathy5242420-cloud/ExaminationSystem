using System.Security.Cryptography;
using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.ForgotPassword;
using ExaminationSystem.Application.Features.Auth.ForgotPassword.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class ForgotPasswordOrchestrator : IForgotPasswordOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public ForgotPasswordOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<ForgotPasswordViewModel>> SendResetCodeAsync(
        ForgotPasswordCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email.Trim();
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<ForgotPasswordViewModel>.Success(
                BuildViewModel(email));
        }

        var resetCode = GenerateOtpCode();
        var otp = new OTP
        {
            UserId = user.Id,
            Code = resetCode,
            Purpose = OtpPurpose.PasswordReset,
            ExpiryDate = DateTime.UtcNow.AddMinutes(10),
            IsUsed = false
        };

        await _unitOfWork.Repository<OTP>().AddAsync(otp, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new PasswordResetRequestedEvent(user.Id, user.Email!, user.FullName, resetCode),
            cancellationToken);

        return Result<ForgotPasswordViewModel>.Success(
            BuildViewModel(user.Email!));
    }

    private static ForgotPasswordViewModel BuildViewModel(string email)
    {
        return new ForgotPasswordViewModel(
            email,
            "If this email exists, a password reset code has been sent.");
    }

    private static string GenerateOtpCode()
    {
        return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
    }
}
