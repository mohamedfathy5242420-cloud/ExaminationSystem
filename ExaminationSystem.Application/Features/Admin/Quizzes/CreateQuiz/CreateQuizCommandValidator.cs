using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.CreateQuiz;

public sealed class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(x => x.DiplomaId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Duration)
            .GreaterThan(0);

        RuleFor(x => x.PassScore)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.MaxAttempts)
            .GreaterThan(0);

        RuleFor(x => x.Instructions)
            .NotEmpty()
            .MaximumLength(2000);
    }
}
