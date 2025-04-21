using System;
using System.Collections.Generic;
using ELearning.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Domain
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int DurationInWeeks { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public bool IsPublished { get; set; }
        public Guid InstructorId { get; set; }

        // Navigation properties
        public virtual User Instructor { get; set; } = null!;
        public virtual ICollection<Module> Modules { get; set; } = new HashSet<Module>();
        public virtual ICollection<User> EnrolledStudents { get; set; } = new HashSet<User>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }

    public enum CourseLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }

    public enum CourseStatus
    {
        Draft,
        Published,
        Archived
    }
} 