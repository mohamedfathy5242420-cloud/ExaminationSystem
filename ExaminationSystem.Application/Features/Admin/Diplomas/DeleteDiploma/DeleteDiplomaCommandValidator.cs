using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Diplomas.DeleteDiploma;

public sealed class DeleteDiplomaCommandValidator : AbstractValidator<DeleteDiplomaCommand>
{
    public DeleteDiplomaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
