using ELearning.Core.Domain;

namespace ELearning.API.DTOs.Course
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public CourseLevel Level { get; set; }
    }
} 
 