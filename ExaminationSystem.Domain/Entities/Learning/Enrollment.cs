using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Identity;

namespace ExaminationSystem.Domain.Entities.Learning;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; set; }

    public Guid DiplomaId { get; set; }

    public DateTime EnrolledAt { get; set; }

    public decimal Progress { get; set; }

    public Student Student { get; set; } = default!;

    public Diploma Diploma { get; set; } = default!;
}
