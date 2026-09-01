using FluentValidation;

namespace ExaminationSystem.Application.Features.Student.Quizzes.AnswerQuestion;

public sealed class AnswerQuestionCommandValidator : AbstractValidator<AnswerQuestionCommand>
{
    public AnswerQuestionCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();
        RuleFor(x => x.AttemptId).NotEmpty();
        RuleFor(x => x.QuestionId).NotEmpty();
        RuleFor(x => x.SelectedOptionId).NotEmpty();
    }
}
