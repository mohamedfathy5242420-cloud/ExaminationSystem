using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Quiz
{
    public class Quizes : BaseEntity, IAggregateRoot
    {
        public Guid DiplomaId { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public int PassScore { get; set; }

        public int MaxAttempts { get; set; }

        public bool IsPublished { get; set; }

        public Diploma Diploma { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
