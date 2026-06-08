using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Domain.Entities.Attempt;

public class QuizAttempt : BaseEntity, IAggregateRoot
{
    public Guid QuizId { get; set; }

    public Guid StudentId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public AttemptStatus Status { get; set; }

    public int Score { get; set; }

    public bool IsPassed { get; set; }

    public QuizEntity Quiz { get; set; } = default!;

    public Student Student { get; set; } = default!;

    public ICollection<AttemptAnswer> Answers { get; set; } = new List<AttemptAnswer>();
}
