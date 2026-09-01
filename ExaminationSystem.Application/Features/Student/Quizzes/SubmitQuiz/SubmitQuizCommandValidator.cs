using FluentValidation;

namespace ExaminationSystem.Application.Features.Student.Quizzes.SubmitQuiz;

public sealed class SubmitQuizCommandValidator : AbstractValidator<SubmitQuizCommand>
{
    public SubmitQuizCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.AttemptId).NotEmpty();
    }
}
