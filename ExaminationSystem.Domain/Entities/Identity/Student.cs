using ExaminationSystem.Domain.Entities.Learning;

namespace ExaminationSystem.Domain.Entities.Identity;

public class Student : User
{
    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}