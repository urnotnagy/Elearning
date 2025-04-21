using System;
using System.Collections.Generic;
using ELearning.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Domain
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TimeLimitInMinutes { get; set; }
        public int PassingScore { get; set; }
        public bool IsPublished { get; set; }
        public Guid ModuleId { get; set; }
        public Guid InstructorId { get; set; }

        // Navigation properties
        public virtual Module Module { get; set; } = null!;
        public virtual User Instructor { get; set; } = null!;
        public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public virtual ICollection<QuizAttempt> Attempts { get; set; } = new HashSet<QuizAttempt>();

        public Quiz()
        {
            Questions = new HashSet<Question>();
            Attempts = new HashSet<QuizAttempt>();
        }
    }
}