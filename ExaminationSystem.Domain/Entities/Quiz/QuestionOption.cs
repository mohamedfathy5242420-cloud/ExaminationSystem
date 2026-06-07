using ExaminationSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Quiz
{
    public class QuestionOption : BaseEntity
    {
        public Guid QuestionId { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}
