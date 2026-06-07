using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Enums;
using ExaminationSystem.Domain.Entities.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Attempt
{
    public class QuizAttempt : BaseEntity, IAggregateRoot
    {
        public Guid QuizId { get; set; }

        public Guid StudentId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public AttemptStatus Status { get; set; }

        public int Score { get; set; }

        public bool IsPassed { get; set; }

        public Quizes Quiz { get; set; }

        public ICollection<AttemptAnswer> Answers { get; set; }
    }
}
