using System;
using System.Collections.Generic;
using ELearning.Core.Common;

namespace ELearning.Core.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }

        // Navigation properties
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Category()
        {
            SubCategories = new HashSet<Category>();
            Courses = new HashSet<Course>();
        }
    }
} 