using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? Grade { get; set; }

        // Navigation properties
        public virtual User Student { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
} 