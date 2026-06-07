using ExaminationSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Domain.Entities.Attempt
{
    public class AttemptAnswer : BaseEntity
    {
        public Guid AttemptId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid? SelectedOptionId { get; set; }

        public string? AnswerText { get; set; }

        public QuizAttempt Attempt { get; set; }
    }
}
