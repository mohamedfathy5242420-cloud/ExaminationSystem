using ExaminationSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Quiz
{
    public class Question : BaseEntity
    {
        public Guid QuizId { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public int Score { get; set; }

        public Quizes Quiz { get; set; }

        public ICollection<QuestionOption> Options { get; set; }
    }
}
