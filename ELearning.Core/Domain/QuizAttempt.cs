using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class QuizAttempt : BaseEntity
    {
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
        public Guid QuizId { get; set; }
        public virtual Quiz? Quiz { get; set; }
        public Guid StudentId { get; set; }
        public virtual User? Student { get; set; }

        // Navigation properties
        public virtual ICollection<Answer> Answers { get; set; }

        public QuizAttempt()
        {
            Answers = new HashSet<Answer>();
        }
    }
} 