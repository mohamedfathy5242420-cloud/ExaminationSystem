using FluentValidation;

namespace ExaminationSystem.Application.Features.Student.Diplomas.EnrollInDiploma;

public sealed class EnrollInDiplomaCommandValidator : AbstractValidator<EnrollInDiplomaCommand>
{
    public EnrollInDiplomaCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.DiplomaId).NotEmpty();
    }
}
