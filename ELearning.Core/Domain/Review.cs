using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public Guid StudentId { get; set; }
        public virtual User? Student { get; set; }
    }
} 