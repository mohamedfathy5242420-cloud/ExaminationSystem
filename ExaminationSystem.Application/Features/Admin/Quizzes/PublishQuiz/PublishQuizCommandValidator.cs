using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Quizzes.PublishQuiz;

public sealed class PublishQuizCommandValidator : AbstractValidator<PublishQuizCommand>
{
    public PublishQuizCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
