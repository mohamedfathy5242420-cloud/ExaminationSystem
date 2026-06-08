using ExaminationSystem.Domain.Common;

namespace ExaminationSystem.Domain.Entities.Identity;

public class OTP : BaseEntity
{
    public Guid UserId { get; set; }

    public string Code { get; set; } = default!;

    public DateTime ExpiryDate { get; set; }

    public bool IsUsed { get; set; }

    public User User { get; set; } = default!;
}
