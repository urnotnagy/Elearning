using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Name => $"{FirstName} {LastName}";
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Course> InstructedCourses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Course> EnrolledCourses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Assignment> Assignments { get; set; } = new HashSet<Assignment>();
        public virtual ICollection<Submission> Submissions { get; set; } = new HashSet<Submission>();
        public virtual ICollection<Quiz> Quizzes { get; set; } = new HashSet<Quiz>();
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new HashSet<QuizAttempt>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Progress> LessonProgress { get; set; } = new HashSet<Progress>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }

    public enum UserRole
    {
        Student,
        Instructor,
        Admin
    }
} 