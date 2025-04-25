using ELearning.API.DTOs.Course;
using ELearning.API.Services;
using ELearning.Core.Domain;
using ELearning.Infrastructure;
using ELearning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ELearning.Tests.Features.Courses
{
    public class CourseTests : IDisposable
    {
        private readonly ICourseService _courseService;
        private readonly Guid _instructorId;
        private readonly Guid _studentId;
        private readonly Guid _programmingCategoryId;
        private readonly Guid _webDevCategoryId;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CourseTests()
        {
            var jwtSettings = new Dictionary<string, string?>
            {
                ["JwtSettings:SecretKey"] = "test-secret-key-for-course-tests",
                ["JwtSettings:Issuer"] = "TestIssuer",
                ["JwtSettings:Audience"] = "TestAudience",
                ["JwtSettings:ExpiryInDays"] = "1"
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(jwtSettings)
                .Build();

            var services = new ServiceCollection();


            services.AddLogging();
            // 1) Add the in-memory DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            // 2) Add Infrastructure services
            services.AddInfrastructure(_configuration);

            services.AddAutoMapper(typeof(ELearning.API.Mapping.MappingProfile));

            // 3) Add other required services
            services.AddScoped<ICourseService, CourseService>();
            // Add other required services...

            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _courseService = serviceProvider.GetRequiredService<ICourseService>();

            // Initialize test data
            _instructorId = Guid.NewGuid();
            _studentId = Guid.NewGuid();
            _programmingCategoryId = Guid.NewGuid();
            _webDevCategoryId = Guid.NewGuid();

            SeedTestData();
        }

        private void SeedTestData()
        {
            // Seed Categories
            var programmingCategoryId = _programmingCategoryId;
            var webDevCategoryId = _webDevCategoryId;
            var dataScienceCategoryId = Guid.NewGuid();
            var frontendCategoryId = Guid.NewGuid();
            var backendCategoryId = Guid.NewGuid();
            var mobileDevCategoryId = Guid.NewGuid();
            var machineLearningCategoryId = Guid.NewGuid();
            var bigDataCategoryId = Guid.NewGuid();

            var categories = new List<Category>
            {
                new Category { 
                    Id = programmingCategoryId, 
                    Name = "Programming", 
                    Description = "Learn programming languages and software development",
                    IconUrl = "https://example.com/icons/programming.png"
                },
                new Category { 
                    Id = webDevCategoryId, 
                    Name = "Web Development", 
                    Description = "Learn web development technologies",
                    IconUrl = "https://example.com/icons/web-dev.png",
                    ParentCategoryId = programmingCategoryId
                },
                new Category { 
                    Id = dataScienceCategoryId, 
                    Name = "Data Science", 
                    Description = "Learn data science and analytics",
                    IconUrl = "https://example.com/icons/data-science.png"
                },
                new Category { 
                    Id = frontendCategoryId, 
                    Name = "Frontend Development", 
                    Description = "Learn frontend technologies like HTML, CSS, and JavaScript",
                    IconUrl = "https://example.com/icons/frontend.png",
                    ParentCategoryId = webDevCategoryId
                },
                new Category { 
                    Id = backendCategoryId, 
                    Name = "Backend Development", 
                    Description = "Learn backend technologies and server-side programming",
                    IconUrl = "https://example.com/icons/backend.png",
                    ParentCategoryId = webDevCategoryId
                },
                new Category { 
                    Id = mobileDevCategoryId, 
                    Name = "Mobile Development", 
                    Description = "Learn mobile app development for iOS and Android",
                    IconUrl = "https://example.com/icons/mobile-dev.png",
                    ParentCategoryId = programmingCategoryId
                },
                new Category { 
                    Id = machineLearningCategoryId, 
                    Name = "Machine Learning", 
                    Description = "Learn machine learning algorithms and techniques",
                    IconUrl = "https://example.com/icons/machine-learning.png",
                    ParentCategoryId = dataScienceCategoryId
                },
                new Category { 
                    Id = bigDataCategoryId, 
                    Name = "Big Data", 
                    Description = "Learn big data processing and analytics",
                    IconUrl = "https://example.com/icons/big-data.png",
                    ParentCategoryId = dataScienceCategoryId
                }
            };
            _context.Categories.AddRange(categories);

            // Seed Users
      //      var instructorId = Guid.NewGuid();
     //       var studentId = Guid.NewGuid();

            var users = new List<User>
            {
                new User
                {
                    Id = _instructorId,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "instructor@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", // Password123!
                    Role = UserRole.Instructor,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = _studentId,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "student@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", // Password123!
                    Role = UserRole.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            _context.Users.AddRange(users);

            // Configure Course-Category relationship
            _context.Database.EnsureCreated();
     //       _context.Database.ExecuteSqlRaw(@"
       //         CREATE TABLE IF NOT EXISTS CourseCategories (
         //           CoursesId uniqueidentifier NOT NULL,
           //         CategoriesId uniqueidentifier NOT NULL,
             //       CONSTRAINT PK_CourseCategories PRIMARY KEY (CoursesId, CategoriesId),
               //     CONSTRAINT FK_CourseCategories_Courses_CoursesId FOREIGN KEY (CoursesId) REFERENCES Courses (Id) ON DELETE CASCADE,
             //       CONSTRAINT FK_CourseCategories_Categories_CategoriesId FOREIGN KEY (CategoriesId) REFERENCES Categories (Id) ON DELETE CASCADE
            //    );");

            // Seed Courses (scalar data only)
            var csharpCourseId = Guid.NewGuid();
            var webDevCourseId = Guid.NewGuid();
            var dataScienceCourseId = Guid.NewGuid();
            var reactCourseId = Guid.NewGuid();
            var nodeJsCourseId = Guid.NewGuid();
            var pythonCourseId = Guid.NewGuid();
            var flutterCourseId = Guid.NewGuid();
            var machineLearningCourseId = Guid.NewGuid();

            var courses = new List<Course>
            {
                new Course
                {
                    Id = csharpCourseId,
                    Title = "Introduction to C#",
                    Description = "Learn the fundamentals of C# programming",
                    Price = 49.99m,
                    DurationInWeeks = 8,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = webDevCourseId,
                    Title = "Web Development Fundamentals",
                    Description = "Learn HTML, CSS, and JavaScript for web development",
                    Price = 59.99m,
                    DurationInWeeks = 10,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = dataScienceCourseId,
                    Title = "Data Science Essentials",
                    Description = "Learn the fundamentals of data science and analytics",
                    Price = 69.99m,
                    DurationInWeeks = 12,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = reactCourseId,
                    Title = "React.js for Frontend Development",
                    Description = "Learn React.js for building modern web applications",
                    Price = 79.99m,
                    DurationInWeeks = 8,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = nodeJsCourseId,
                    Title = "Node.js Backend Development",
                    Description = "Learn Node.js for building server-side applications",
                    Price = 79.99m,
                    DurationInWeeks = 8,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = pythonCourseId,
                    Title = "Python for Data Science",
                    Description = "Learn Python programming for data science",
                    Price = 59.99m,
                    DurationInWeeks = 10,
                    Level = CourseLevel.Beginner,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = flutterCourseId,
                    Title = "Flutter Mobile App Development",
                    Description = "Learn Flutter for building cross-platform mobile apps",
                    Price = 89.99m,
                    DurationInWeeks = 12,
                    Level = CourseLevel.Intermediate,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Id = machineLearningCourseId,
                    Title = "Machine Learning Fundamentals",
                    Description = "Learn the fundamentals of machine learning",
                    Price = 99.99m,
                    DurationInWeeks = 14,
                    Level = CourseLevel.Advanced,
                    Status = CourseStatus.Published,
                    IsPublished = true,
                    InstructorId = _instructorId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            _context.Courses.AddRange(courses);



            // --- now wire up the many-to-many in memory via nav props:
            var joinData = new List<(Guid CourseId, Guid CategoryId)> {
    (csharpCourseId, _programmingCategoryId),
    (webDevCourseId,  _webDevCategoryId),
    // … etc …
};

            foreach (var (courseId, categoryId) in joinData)
            {
                var course = _context.Courses.Find(courseId)!;
                var category = _context.Categories.Find(categoryId)!;
                course.Categories.Add(category);
            }



            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllCourses_ReturnsSeededCourses()
        {
            // Act
            var courses = await _courseService.GetAllCoursesAsync();

            // Assert
            Assert.NotNull(courses);
            Assert.NotEmpty(courses);
            Assert.Contains(courses, c => c.Title == "Introduction to C#");
            Assert.Contains(courses, c => c.Title == "Web Development Fundamentals");
        }

        [Fact]
        public async Task GetCourseById_WithValidId_ReturnsCourse()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();

            // Act
            var result = await _courseService.GetCourseByIdAsync(course.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(course.Id, result.Id);
            Assert.Equal(course.Title, result.Title);
            Assert.Equal(course.Description, result.Description);
            Assert.Equal(course.Price, result.Price);
            Assert.Equal(course.DurationInWeeks, result.DurationInWeeks);
            Assert.Equal(course.Level, result.Level);
            Assert.Equal(course.Status, result.Status);
            Assert.Equal(course.IsPublished, result.IsPublished);
            Assert.Equal(course.InstructorId, result.InstructorId);
        }

        [Fact]
        public async Task GetCourseById_WithInvalidId_ReturnsNull()
        {
            // Act
            var result = await _courseService.GetCourseByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCourse_WithValidData_CreatesCourse()
        {
            // Arrange
            var createCourseDto = new CreateCourseDto
            {
                Name = "New Course",
                Description = "Course Description",
                Price = 99.99m,
                DurationInWeeks = 12,
                Level = CourseLevel.Intermediate,
                Status = CourseStatus.Draft,
                IsPublished = false,
                CategoryIds = new List<Guid> { _programmingCategoryId, _webDevCategoryId }
            };

            // Act
            var result = await _courseService.CreateCourseAsync(createCourseDto, _instructorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createCourseDto.Name, result.Title);
            Assert.Equal(createCourseDto.Description, result.Description);
            Assert.Equal(createCourseDto.Price, result.Price);
            Assert.Equal(createCourseDto.DurationInWeeks, result.DurationInWeeks);
            Assert.Equal(createCourseDto.Level, result.Level);
            Assert.Equal(createCourseDto.Status, result.Status);
            Assert.Equal(createCourseDto.IsPublished, result.IsPublished);
            Assert.Equal(_instructorId, result.InstructorId);
            Assert.Equal(2, result.CategoryNames.Count);
        }

        [Fact]
        public async Task UpdateCourse_WithValidData_UpdatesCourse()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();
            var updateCourseDto = new UpdateCourseDto
            {
                Name = "Updated Title",
                Description = "Updated Description",
                Price = 149.99m,
                DurationInWeeks = 16,
                Level = CourseLevel.Advanced,
                Status = CourseStatus.Published,
                IsPublished = true,
                CategoryIds = new List<Guid> { _programmingCategoryId }
            };

            // Act
            var result = await _courseService.UpdateCourseAsync(course.Id, updateCourseDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateCourseDto.Name, result.Title);
            Assert.Equal(updateCourseDto.Description, result.Description);
            Assert.Equal(updateCourseDto.Price, result.Price);
            Assert.Equal(updateCourseDto.DurationInWeeks, result.DurationInWeeks);
            Assert.Equal(updateCourseDto.Level, result.Level);
            Assert.Equal(updateCourseDto.Status, result.Status);
            Assert.Equal(updateCourseDto.IsPublished, result.IsPublished);
            Assert.Single(result.CategoryNames);
        }

        [Fact]
        public async Task DeleteCourse_WithValidId_DeletesCourse()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();

            // Act
            await _courseService.DeleteCourseAsync(course.Id);

            // Assert
            var deletedCourse = await _courseService.GetCourseByIdAsync(course.Id);
            Assert.Null(deletedCourse);
        }

        [Fact]
        public async Task EnrollStudent_WithValidIds_EnrollsStudent()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();

            // Act
            await _courseService.EnrollStudentAsync(course.Id, _studentId);

            // Assert
            var studentCourses = await _courseService.GetStudentCoursesAsync(_studentId);
            Assert.Contains(studentCourses, c => c.Id == course.Id);
        }

        [Fact]
        public async Task UnenrollStudent_WithValidIds_UnenrollsStudent()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();
            await _courseService.EnrollStudentAsync(course.Id, _studentId);

            // Act
            await _courseService.UnenrollStudentAsync(course.Id, _studentId);

            // Assert
            var studentCourses = await _courseService.GetStudentCoursesAsync(_studentId);
            Assert.DoesNotContain(studentCourses, c => c.Id == course.Id);
        
            
        
        }

        [Fact]
        public async Task GetInstructorCourses_ReturnsInstructorCourses()
        {
            // Act
            var courses = await _courseService.GetInstructorCoursesAsync(_instructorId);

            // Assert
            Assert.NotNull(courses);
            Assert.NotEmpty(courses);
            Assert.All(courses, c => Assert.Equal(_instructorId, c.InstructorId));
        }

        [Fact]
        public async Task GetStudentCourses_ReturnsEnrolledCourses()
        {
            // Arrange
            var courses = await _courseService.GetAllCoursesAsync();
            var course = courses.First();
            await _courseService.EnrollStudentAsync(course.Id, _studentId);

            // Act
            var studentCourses = await _courseService.GetStudentCoursesAsync(_studentId);

            // Assert
            Assert.NotNull(studentCourses);
            Assert.NotEmpty(studentCourses);
            Assert.Contains(studentCourses, c => c.Id == course.Id);
        }
    }
} 