using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Questions.CreateQuestion;

public sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.QuizId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Explanation).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Score).GreaterThan(0);
        RuleFor(x => x.Options)
            .NotNull()
            .Must(options => options.Count >= 2)
            .WithMessage("Question must contain at least two options.")
            .Must(options => options.Count(option => option.IsCorrect) == 1)
            .WithMessage("Question must contain exactly one correct option.");

        RuleForEach(x => x.Options).ChildRules(option =>
        {
            option.RuleFor(x => x.Text)
                .NotEmpty()
                .MaximumLength(1000);
        });
    }
}
