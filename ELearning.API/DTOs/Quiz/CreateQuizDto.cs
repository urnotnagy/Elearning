using System.ComponentModel.DataAnnotations;
using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.Quiz
{
    public class CreateQuizDto : BaseCreateDto
    {
        [Required]
        [Range(1, 180)]
        public int TimeLimitInMinutes { get; set; }

        [Required]
        [Range(0, 100)]
        public int PassingScore { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        public Guid ModuleId { get; set; }

        public ICollection<CreateQuestionDto> Questions { get; set; } = new List<CreateQuestionDto>();
    }

    public class CreateQuestionDto
    {
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        public int Points { get; set; }

        public ICollection<CreateOptionDto> Options { get; set; } = new List<CreateOptionDto>();
    }

    public class CreateOptionDto
    {
        [Required]
        [StringLength(200)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
} 