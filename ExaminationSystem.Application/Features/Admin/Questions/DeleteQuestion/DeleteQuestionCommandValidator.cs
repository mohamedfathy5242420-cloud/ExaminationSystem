using FluentValidation;

namespace ExaminationSystem.Application.Features.Admin.Questions.DeleteQuestion;

public sealed class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
