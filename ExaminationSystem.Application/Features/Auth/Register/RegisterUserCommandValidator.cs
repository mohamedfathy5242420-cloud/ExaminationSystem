using FluentValidation;

namespace ExaminationSystem.Application.Features.Auth.Register;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private static readonly string[] SupportedUserTypes =
    [
        "student",
        "instructor",
        "admin"
    ];

    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.UserType)
            .NotEmpty()
            .Must(userType => SupportedUserTypes.Contains(userType.Trim().ToLowerInvariant()))
            .WithMessage("User type must be Student, Instructor, or Admin.");
    }
}
