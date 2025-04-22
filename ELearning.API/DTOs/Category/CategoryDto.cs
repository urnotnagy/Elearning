using System;
using System.Collections.Generic;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.Category
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public ICollection<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    }

    public class CategoryCreateDto : BaseCreateDto
    {
        public string IconUrl { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }

    public class CategoryUpdateDto : BaseUpdateDto
    {
        public string IconUrl { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
