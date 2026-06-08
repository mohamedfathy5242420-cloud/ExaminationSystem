using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Identity;
using QuizEntity = ExaminationSystem.Domain.Entities.Quiz.Quiz;

namespace ExaminationSystem.Domain.Entities.Learning;

public class Diploma : BaseEntity, IAggregateRoot
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool IsPublished { get; set; }

    public Guid InstructorId { get; set; }

    public Instructor Instructor { get; set; } = default!;

    public ICollection<QuizEntity> Quizzes { get; set; } = new List<QuizEntity>();

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
