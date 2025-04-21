using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Question : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public int Points { get; set; }
        public Guid QuizId { get; set; }
        public virtual Quiz? Quiz { get; set; }

        // Navigation properties
        public virtual ICollection<Option> Options { get; set; }

        public Question()
        {
            Options = new HashSet<Option>();
        }
    }

    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        ShortAnswer,
        Essay
    }
} 