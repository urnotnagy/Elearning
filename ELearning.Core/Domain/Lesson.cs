using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
        public int DurationInMinutes { get; set; }
        public Guid ModuleId { get; set; }
        public virtual Module? Module { get; set; }

        // Navigation properties
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<Progress> StudentProgress { get; set; }

        public Lesson()
        {
            Resources = new HashSet<Resource>();
            StudentProgress = new HashSet<Progress>();
        }
    }
} 