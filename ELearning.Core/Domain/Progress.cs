using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Progress : BaseEntity
    {
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int TimeSpent { get; set; } // in minutes
        public Guid LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public Guid StudentId { get; set; }
        public virtual User? Student { get; set; }
    }
} 