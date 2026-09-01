using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.UnpublishQuiz;

public sealed class UnpublishQuizCommandValidator : AbstractValidator<UnpublishQuizCommand>
{
    public UnpublishQuizCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
