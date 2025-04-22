using System;
using System.Collections.Generic;
using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.Course
{
    public class CourseDto : BaseDto
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
        public string InstructorName { get; set; } = string.Empty;
        public int EnrolledStudentsCount { get; set; }
        public double AverageRating { get; set; }
        public ICollection<string> CategoryNames { get; set; } = new List<string>();
    }
} 
 
