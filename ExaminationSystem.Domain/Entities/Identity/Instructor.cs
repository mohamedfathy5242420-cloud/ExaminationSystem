using ExaminationSystem.Domain.Entities.Learning;

namespace ExaminationSystem.Domain.Entities.Identity;

public class Instructor : User
{
    public ICollection<Diploma> Diplomas { get; set; } = new List<Diploma>();
}
