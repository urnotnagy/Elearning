using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Module : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }

        // Navigation properties
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }

        public Module()
        {
            Lessons = new HashSet<Lesson>();
            Assignments = new HashSet<Assignment>();
            Quizzes = new HashSet<Quiz>();
            Resources = new HashSet<Resource>();
        }
    }
} 