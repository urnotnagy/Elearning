using System;
using System.Collections.Generic;
using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.Quiz
{
    public class QuizDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TimeLimitInMinutes { get; set; }
        public int PassingScore { get; set; }
        public bool IsPublished { get; set; }
        public Guid ModuleId { get; set; }
        public string ModuleTitle { get; set; } = string.Empty;
        public Guid InstructorId { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public int QuestionsCount { get; set; }
        public int AttemptsCount { get; set; }
        public double AverageScore { get; set; }
    }
} 