using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.CreateDiploma;

public sealed class CreateDiplomaCommandValidator : AbstractValidator<CreateDiplomaCommand>
{
    public CreateDiplomaCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.InstructorId)
            .NotEmpty();
    }
}
