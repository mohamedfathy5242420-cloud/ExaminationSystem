using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Quiz;

namespace ExaminationSystem.Domain.Entities.Attempt;

public class AttemptAnswer : BaseEntity
{
    public Guid AttemptId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid? SelectedOptionId { get; set; }

    public string? AnswerText { get; set; }

    public QuizAttempt Attempt { get; set; } = default!;

    public Question Question { get; set; } = default!;

    public QuestionOption? SelectedOption { get; set; }
}
