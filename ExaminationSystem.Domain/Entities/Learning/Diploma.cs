using ExaminationSystem.Domain.Common;
using ExaminationSystem.Domain.Entities.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Learning
{
    public class Diploma : BaseEntity, IAggregateRoot
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public Guid InstructorId { get; set; }

        public ICollection<Quizes> Quizzes { get; set; }
    }
}
