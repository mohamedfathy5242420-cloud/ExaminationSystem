using ExaminationSystem.Domain.Common;

namespace ExaminationSystem.Domain.Entities.Quiz;

public class Question : BaseEntity
{
    public Guid QuizId { get; set; }

    public string Text { get; set; } = default!;

    public int Order { get; set; }

    public int Score { get; set; }

    public Quiz Quiz { get; set; } = default!;

    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
}
