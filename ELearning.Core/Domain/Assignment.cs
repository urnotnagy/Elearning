using System;
using System.Collections.Generic;
using ELearning.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Domain
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int MaxPoints { get; set; }
        public Guid ModuleId { get; set; }
        public Guid InstructorId { get; set; }

        // Navigation properties
        public virtual Module Module { get; set; } = null!;
        public virtual User Instructor { get; set; } = null!;
        public virtual ICollection<Submission> Submissions { get; set; } = new HashSet<Submission>();

        public Assignment()
        {
            Submissions = new HashSet<Submission>();
        }
    }
} 