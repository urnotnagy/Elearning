using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Option : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
} 