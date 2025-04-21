using System;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Resource : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public ResourceType Type { get; set; }
        public Guid? LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public Guid? ModuleId { get; set; }
        public virtual Module? Module { get; set; }
    }

    public enum ResourceType
    {
        Document,
        Video,
        Audio,
        Link,
        Other
    }
} 