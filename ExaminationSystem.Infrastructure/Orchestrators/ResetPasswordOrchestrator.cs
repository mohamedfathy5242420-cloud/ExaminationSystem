using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.ResetPassword;
using ExaminationSystem.Application.Features.Auth.ResetPassword.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class ResetPasswordOrchestrator : IResetPasswordOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public ResetPasswordOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<ResetPasswordViewModel>> ResetAsync(
        ResetPasswordCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email.Trim();
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<ResetPasswordViewModel>.Failure("Invalid email or OTP code.");
        }

        var otp = _unitOfWork.Repository<OTP>()
            .Query()
            .Where(x => x.UserId == user.Id &&
                        x.Purpose == OtpPurpose.PasswordReset &&
                        !x.IsUsed)
            .OrderByDescending(x => x.CreatedOnUtc)
            .FirstOrDefault();

        if (otp is null)
        {
            return Result<ResetPasswordViewModel>.Failure("Invalid email or OTP code.");
        }

        if (otp.ExpiryDate < DateTime.UtcNow)
        {
            return Result<ResetPasswordViewModel>.Failure("OTP code has expired.");
        }

        if (otp.Code != command.OtpCode.Trim())
        {
            return Result<ResetPasswordViewModel>.Failure("Invalid email or OTP code.");
        }

        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
        if (!removePasswordResult.Succeeded)
        {
            return Result<ResetPasswordViewModel>.Failure(
                string.Join(", ", removePasswordResult.Errors.Select(x => x.Description)));
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, command.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            return Result<ResetPasswordViewModel>.Failure(
                string.Join(", ", addPasswordResult.Errors.Select(x => x.Description)));
        }

        otp.IsUsed = true;
        _unitOfWork.Repository<OTP>().Update(otp);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new PasswordResetCompletedEvent(user.Id, user.Email!, user.FullName),
            cancellationToken);

        var viewModel = new ResetPasswordViewModel(
            user.Email!,
            "Password reset successfully.");

        return Result<ResetPasswordViewModel>.Success(viewModel);
    }
}
