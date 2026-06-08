using ExaminationSystem.Domain.Common;

namespace ExaminationSystem.Domain.Entities.Quiz;

public class QuestionOption : BaseEntity
{
    public Guid QuestionId { get; set; }

    public string Text { get; set; } = default!;

    public bool IsCorrect { get; set; }

    public Question Question { get; set; } = default!;
}
