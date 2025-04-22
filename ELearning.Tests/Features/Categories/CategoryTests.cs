using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ELearning.API.DTOs.Category;
using ELearning.API.Services;
using ELearning.Core.Domain;
using ELearning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ELearning.Tests.Features.Categories
{
    public class CategoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryTests()
        {
            // Create a new in-memory database for each test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            
            // Setup AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryCreateDto, Category>();
                cfg.CreateMap<CategoryUpdateDto, Category>();
            });
            _mapper = config.CreateMapper();

            // Setup logger
            _logger = Mock.Of<ILogger<CategoryService>>();

            _categoryService = new CategoryService(_context, _mapper, _logger);

            // Seed test data
            SeedTestData();
        }

        private void SeedTestData()
        {
            var categories = new[]
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Programming",
                    Description = "Programming courses",
                    IconUrl = "https://example.com/programming.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Design",
                    Description = "Design courses",
                    IconUrl = "https://example.com/design.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Programming");
            Assert.Contains(result, c => c.Name == "Design");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCategory()
        {
            // Arrange
            var category = _context.Categories.First();

            // Act
            var result = await _categoryService.GetByIdAsync(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
            Assert.Equal(category.IconUrl, result.IconUrl);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            // Act
            var result = await _categoryService.GetByIdAsync(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_CreatesCategory()
        {
            // Arrange
            var dto = new CategoryCreateDto
            {
                Name = "New Category",
                Description = "New category description",
                IconUrl = "https://example.com/new-category.png"
            };

            // Act
            var result = await _categoryService.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.IconUrl, result.IconUrl);
            Assert.NotEqual(Guid.Empty, result.Id);

            // Verify it was actually saved
            var savedCategory = await _context.Categories.FindAsync(result.Id);
            Assert.NotNull(savedCategory);
            Assert.Equal(dto.Name, savedCategory.Name);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_UpdatesCategory()
        {
            // Arrange
            var category = _context.Categories.First();
            var dto = new CategoryUpdateDto
            {
                Name = "Updated Category",
                Description = "Updated description",
                IconUrl = "https://example.com/updated.png"
            };

            // Act
            await _categoryService.UpdateAsync(category.Id, dto);

            // Assert
            var updatedCategory = await _context.Categories.FindAsync(category.Id);
            Assert.NotNull(updatedCategory);
            Assert.Equal(dto.Name, updatedCategory.Name);
            Assert.Equal(dto.Description, updatedCategory.Description);
            Assert.Equal(dto.IconUrl, updatedCategory.IconUrl);
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var dto = new CategoryUpdateDto
            {
                Name = "Updated Category",
                Description = "Updated description",
                IconUrl = "https://example.com/updated.png"
            };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _categoryService.UpdateAsync(Guid.NewGuid(), dto));
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesCategory()
        {
            // Arrange
            var category = _context.Categories.First();

            // Act
            await _categoryService.DeleteAsync(category.Id);

            // Assert
            Assert.Null(await _context.Categories.FindAsync(category.Id));
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _categoryService.DeleteAsync(Guid.NewGuid()));
        }
    }
} 