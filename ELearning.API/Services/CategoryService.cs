using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ELearning.API.DTOs.Category;
using ELearning.Core.Domain;
using ELearning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELearning.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .Include(c => c.Courses)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all categories");
                throw;
            }
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found", id);
                    return null;
                }

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category with ID {CategoryId}", id);
                throw;
            }
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            try
            {
                var category = _mapper.Map<Category>(dto);
                
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created new category with ID {CategoryId}", category.Id);
                
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task UpdateAsync(Guid id, CategoryUpdateDto dto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for update", id);
                    throw new KeyNotFoundException($"Category with ID {id} not found");
                }

                _mapper.Map(dto, category);
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Updated category with ID {CategoryId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with ID {CategoryId}", id);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var category = await _context.Categories
                    .Include(c => c.SubCategories)
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync(c => c.Id == id);
                
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found for deletion", id);
                    throw new KeyNotFoundException($"Category with ID {id} not found");
                }

                // Check if category has subcategories or courses
                if (category.SubCategories.Any() || category.Courses.Any())
                {
                    _logger.LogWarning("Cannot delete category with ID {CategoryId} because it has subcategories or courses", id);
                    throw new InvalidOperationException("Cannot delete category with subcategories or courses");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Deleted category with ID {CategoryId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", id);
                throw;
            }
        }
    }
}
