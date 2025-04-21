using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Answer : BaseEntity
    {
        public string Response { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }
        public Guid QuizAttemptId { get; set; }
        public virtual QuizAttempt? QuizAttempt { get; set; }
    }
} 