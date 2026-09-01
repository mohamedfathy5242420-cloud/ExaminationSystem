using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Attempt;
using ExaminationSystem.Domain.Entities.Learning;

namespace ExaminationSystem.Domain.Entities.Quiz;

public class Quiz : BaseEntity, IAggregateRoot
{
    public Guid DiplomaId { get; set; }

    public string Title { get; set; } = default!;

    public int Duration { get; set; }

    public int PassScore { get; set; }

    public int MaxAttempts { get; set; }

    public string Instructions { get; set; } = default!;

    public bool IsPublished { get; set; }

    public Diploma Diploma { get; set; } = default!;

    public ICollection<Question> Questions { get; set; } = new List<Question>();

    public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
}
