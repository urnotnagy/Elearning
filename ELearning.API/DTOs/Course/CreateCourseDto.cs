using System.ComponentModel.DataAnnotations;
using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.Course
{
    public class CreateCourseDto : BaseCreateDto
    {
        public string? ImageUrl { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 52)]
        public int DurationInWeeks { get; set; }

        [Required]
        public CourseLevel Level { get; set; }

        public CourseStatus Status { get; set; } = CourseStatus.Draft;

        public bool IsPublished { get; set; }

        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
} 
 