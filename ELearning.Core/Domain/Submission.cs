using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Submission : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int? Score { get; set; }
        public string? Feedback { get; set; }
        public Guid AssignmentId { get; set; }
        public virtual Assignment? Assignment { get; set; }
        public Guid StudentId { get; set; }
        public virtual User? Student { get; set; }
    }
} 