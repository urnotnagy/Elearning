using ELearning.Core.Domain;

namespace ELearning.API.DTOs.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public UserRole RequiredRole { get; set; }
        public Guid InstructorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 
 
