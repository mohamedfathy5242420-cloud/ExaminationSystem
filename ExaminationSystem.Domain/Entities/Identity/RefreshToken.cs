using ExaminationSystem.Domain.Common;

namespace ExaminationSystem.Domain.Entities.Identity;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = default!;

    public DateTime ExpiryDate { get; set; }

    public bool Revoked { get; set; }

    public User User { get; set; } = default!;
}
