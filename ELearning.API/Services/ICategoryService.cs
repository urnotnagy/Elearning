using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ELearning.API.DTOs.Category;

namespace ELearning.API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(Guid id, CategoryUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
