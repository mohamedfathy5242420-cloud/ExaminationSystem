using FluentValidation;

namespace ExaminationSystem.Application.Features.Student.Quizzes.StartQuiz;

public sealed class StartQuizCommandValidator : AbstractValidator<StartQuizCommand>
{
    public StartQuizCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.QuizId).NotEmpty();
    }
}
