using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ELearning.API.DTOs.Auth;
using ELearning.API.DTOs.Category;
using ELearning.Core.Domain;
using ELearning.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ELearning.Tests.Features.Categories
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _dbName;

        public CategoryControllerTests(WebApplicationFactory<Program> factory)
        {
            _dbName = $"CategoryTestDb_{Guid.NewGuid()}";
            var jwtSettings = new Dictionary<string, string?>
            {
                ["JwtSettings:SecretKey"] = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret",
                ["JwtSettings:Issuer"] = "http://localhost:5000",
                ["JwtSettings:Audience"] = "http://localhost:5000",
                ["JwtSettings:ExpiryInDays"] = "1"
            };

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((ctx, cfg) =>
                {
                    cfg.AddInMemoryCollection(jwtSettings);
                });

                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(_dbName);
                    });
                });
            });

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private async Task<CategoryDto> SeedTestCategory()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test Category",
                Description = "Test Description",
                IconUrl = "https://example.com/test.png",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IconUrl = category.IconUrl,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        private async Task<HttpClient> GetAdminClient()
        {
            var client = _factory.CreateClient();

            var registerDto = new RegisterDto
            {
                Email = "admin@test.com",
                Password = "Admin123!@#",
                FirstName = "Admin",
                LastName = "User",
                Role = UserRole.Admin
            };

            var registerResponse = await client.PostAsync(
                "/api/auth/register",
                new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json")
            );

            if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var loginDto = new LoginDto
                {
                    Email = "admin@test.com",
                    Password = "Admin123!@#"
                };

                var loginResponse = await client.PostAsync(
                    "/api/auth/login",
                    new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json")
                );

                Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
                var loginResult = JsonSerializer.Deserialize<LoginResponseDto>(
                    await loginResponse.Content.ReadAsStringAsync(),
                    _jsonOptions
                );
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", loginResult?.Token);
            }
            else
            {
                Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);
                var registerResult = JsonSerializer.Deserialize<LoginResponseDto>(
                    await registerResponse.Content.ReadAsStringAsync(),
                    _jsonOptions
                );
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", registerResult?.Token);
            }

            return client;
        }

        private async Task<HttpClient> GetStudentClient()
        {
            var client = _factory.CreateClient();

            var registerDto = new RegisterDto
            {
                Email = "student@test.com",
                Password = "Student123!@#",
                FirstName = "Student",
                LastName = "User",
                Role = UserRole.Student
            };

            var registerResponse = await client.PostAsync(
                "/api/auth/register",
                new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json")
            );

            if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var loginDto = new LoginDto
                {
                    Email = "student@test.com",
                    Password = "Student123!@#"
                };

                var loginResponse = await client.PostAsync(
                    "/api/auth/login",
                    new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json")
                );

                Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
                var loginResult = JsonSerializer.Deserialize<LoginResponseDto>(
                    await loginResponse.Content.ReadAsStringAsync(),
                    _jsonOptions
                );
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", loginResult?.Token);
            }
            else
            {
                Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);
                var registerResult = JsonSerializer.Deserialize<LoginResponseDto>(
                    await registerResponse.Content.ReadAsStringAsync(),
                    _jsonOptions
                );
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", registerResult?.Token);
            }

            return client;
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithCategories()
        {
            // Arrange
            var client = _factory.CreateClient();
            await SeedTestCategory();

            // Act
            var response = await client.GetAsync("/api/category");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryDto>>(content, _jsonOptions);
            Assert.NotNull(categories);
            Assert.Single(categories);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();
            var seededCategory = await SeedTestCategory();

            // Act
            var response = await client.GetAsync($"/api/category/{seededCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CategoryDto>(content, _jsonOptions);
            Assert.NotNull(result);
            Assert.Equal(seededCategory.Id, result.Id);
            Assert.Equal(seededCategory.Name, result.Name);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var invalidId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/api/category/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithoutAuth_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();
            var category = new CategoryCreateDto
            {
                Name = "Test Category",
                Description = "Test Description",
                IconUrl = "https://example.com/test.png"
            };

            // Act
            var response = await client.PostAsync(
                "/api/category",
                new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json")
            );

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithStudentToken_ReturnsForbidden()
        {
            // Arrange
            var client = await GetStudentClient();
            var category = new CategoryCreateDto
            {
                Name = "Test Category",
                Description = "Test Description",
                IconUrl = "https://example.com/test.png"
            };

            // Act
            var response = await client.PostAsync(
                "/api/category",
                new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json")
            );

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithAdminToken_ReturnsCreated()
        {
            // Arrange
            var client = await GetAdminClient();
            var category = new CategoryCreateDto
            {
                Name = "Test Category",
                Description = "Test Description",
                IconUrl = "https://example.com/test.png"
            };

            // Act
            var response = await client.PostAsync(
                "/api/category",
                new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json")
            );

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CategoryDto>(content, _jsonOptions);
            Assert.NotNull(result);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
            Assert.Equal(category.IconUrl, result.IconUrl);
        }

        [Fact]
        public async Task Update_WithValidData_ReturnsNoContent()
        {
            // Arrange
            var client = await GetAdminClient();
            var seededCategory = await SeedTestCategory();
            var updateDto = new CategoryUpdateDto
            {
                Name = "Updated Category",
                Description = "Updated Description",
                IconUrl = "https://example.com/updated.png"
            };

            // Act
            var response = await client.PutAsync(
                $"/api/category/{seededCategory.Id}",
                new StringContent(JsonSerializer.Serialize(updateDto), Encoding.UTF8, "application/json")
            );

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = await GetAdminClient();
            var invalidId = Guid.NewGuid();
            var updateDto = new CategoryUpdateDto
            {
                Name = "Updated Category",
                Description = "Updated Description",
                IconUrl = "https://example.com/updated.png"
            };

            // Act
            var response = await client.PutAsync(
                $"/api/category/{invalidId}",
                new StringContent(JsonSerializer.Serialize(updateDto), Encoding.UTF8, "application/json")
            );

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithoutAuth_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();
            var seededCategory = await SeedTestCategory();

            // Act
            var response = await client.DeleteAsync($"/api/category/{seededCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithStudentToken_ReturnsForbidden()
        {
            // Arrange
            var client = await GetStudentClient();
            var seededCategory = await SeedTestCategory();

            // Act
            var response = await client.DeleteAsync($"/api/category/{seededCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithAdminToken_ReturnsNoContent()
        {
            // Arrange
            var client = await GetAdminClient();
            var seededCategory = await SeedTestCategory();

            // Act
            var response = await client.DeleteAsync($"/api/category/{seededCategory.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
} 