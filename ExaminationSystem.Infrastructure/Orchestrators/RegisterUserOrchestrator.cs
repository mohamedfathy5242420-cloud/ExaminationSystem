using System.Security.Cryptography;
using ExaminationSystem.Application.Common;
using ExaminationSystem.Application.Common.Events;
using ExaminationSystem.Application.Features.Auth.Register;
using ExaminationSystem.Application.Features.Auth.Register.ViewModels;
using ExaminationSystem.Application.Interfaces;
using ExaminationSystem.Application.Interfaces.Orchestrators;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Infrastructure.Orchestrators;

public class RegisterUserOrchestrator : IRegisterUserOrchestrator
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventDispatcher _eventDispatcher;

    public RegisterUserOrchestrator(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IEventDispatcher eventDispatcher)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Result<RegisterUserViewModel>> RegisterAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email.Trim();
        if (await _userManager.FindByEmailAsync(email) is not null)
        {
            return Result<RegisterUserViewModel>.Failure("Email is already registered.");
        }

        var user = CreateUser(command);
        if (user is null)
        {
            return Result<RegisterUserViewModel>.Failure("Invalid user type.");
        }

        var identityResult = await _userManager.CreateAsync(user, command.Password);
        if (!identityResult.Succeeded)
        {
            var error = string.Join(", ", identityResult.Errors.Select(x => x.Description));
            return Result<RegisterUserViewModel>.Failure(error);
        }

        var otpCode = GenerateOtpCode();
        var otp = new OTP
        {
            UserId = user.Id,
            Code = otpCode,
            Purpose = OtpPurpose.AccountVerification,
            ExpiryDate = DateTime.UtcNow.AddMinutes(10),
            IsUsed = false
        };

        await _unitOfWork.Repository<OTP>().AddAsync(otp, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(
            new UserRegisteredEvent(user.Id, user.Email!, user.FullName, otpCode),
            cancellationToken);

        var viewModel = new RegisterUserViewModel(
            user.Id,
            user.Email!,
            user.FullName,
            command.UserType.Trim().ToLowerInvariant(),
            user.Status.ToString(),
            "Account created. Please verify the OTP sent to your email.");

        return Result<RegisterUserViewModel>.Success(viewModel);
    }

    private static User? CreateUser(RegisterUserCommand command)
    {
        User? user = command.UserType.Trim().ToLowerInvariant() switch
        {
            "student" => new Student(),
            "instructor" => new Instructor(),
            "admin" => new Admin(),
            _ => null
        };

        if (user is null)
        {
            return null;
        }

        var email = command.Email.Trim();

        user.FullName = command.FullName.Trim();
        user.Email = email;
        user.UserName = email;
        user.Status = UserStatus.Pending;
        user.EmailConfirmed = false;

        return user;
    }

    private static string GenerateOtpCode()
    {
        return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
    }
}
