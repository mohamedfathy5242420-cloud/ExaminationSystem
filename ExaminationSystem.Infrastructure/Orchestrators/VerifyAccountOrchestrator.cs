using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.VerifyAccount;
using ExaminationSystem.Application.Features.Auth.VerifyAccount.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class VerifyAccountOrchestrator : IVerifyAccountOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public VerifyAccountOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<VerifyAccountViewModel>> VerifyAsync(
        VerifyAccountCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email.Trim();
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<VerifyAccountViewModel>.Failure("Invalid email or OTP code.");
        }

        if (user.Status == UserStatus.Active && user.EmailConfirmed)
        {
            return Result<VerifyAccountViewModel>.Failure("Account is already verified.");
        }

        if (user.Status != UserStatus.Pending)
        {
            return Result<VerifyAccountViewModel>.Failure("Account cannot be verified in its current status.");
        }

        var otp = _unitOfWork.Repository<OTP>()
            .Query()
            .Where(x => x.UserId == user.Id &&
                        x.Purpose == OtpPurpose.AccountVerification &&
                        !x.IsUsed)
            .OrderByDescending(x => x.CreatedOnUtc)
            .FirstOrDefault();

        if (otp is null)
        {
            return Result<VerifyAccountViewModel>.Failure("Invalid email or OTP code.");
        }

        if (otp.ExpiryDate < DateTime.UtcNow)
        {
            return Result<VerifyAccountViewModel>.Failure("OTP code has expired.");
        }

        if (otp.Code != command.OtpCode.Trim())
        {
            return Result<VerifyAccountViewModel>.Failure("Invalid email or OTP code.");
        }

        otp.IsUsed = true;
        user.Status = UserStatus.Active;
        user.EmailConfirmed = true;

        _unitOfWork.Repository<OTP>().Update(otp);
        await _userManager.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new AccountVerifiedEvent(user.Id, user.Email!, user.FullName),
            cancellationToken);

        var viewModel = new VerifyAccountViewModel(
            user.Id,
            user.Email!,
            user.Status.ToString(),
            "Account verified successfully.");

        return Result<VerifyAccountViewModel>.Success(viewModel);
    }
}
