using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Domain.Entities.Identity;

public abstract class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;

    public UserStatus Status { get; set; }

    public ICollection<OTP> OTPs { get; set; } = new List<OTP>();

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}