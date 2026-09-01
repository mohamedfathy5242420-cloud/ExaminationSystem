using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.UpdateDiploma;

public sealed class UpdateDiplomaCommandValidator : AbstractValidator<UpdateDiplomaCommand>
{
    public UpdateDiplomaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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
