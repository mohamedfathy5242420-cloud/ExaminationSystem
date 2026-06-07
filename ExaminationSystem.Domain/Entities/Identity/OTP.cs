using ExaminationSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Identity
{
    public class OTP : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Code { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }

        public User User { get; set; }
    }
}
