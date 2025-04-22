using ELearning.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ELearning.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Seed Categories
            var programmingCategoryId = Guid.NewGuid();
            var webDevCategoryId = Guid.NewGuid();
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
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Users
            var instructorId = Guid.NewGuid();
            var studentId = Guid.NewGuid();

            var users = new List<User>
            {
                new User
                {
                    Id = instructorId,
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
                    Id = studentId,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "student@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", // Password123!
                    Role = UserRole.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            // Seed Courses
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
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
                    InstructorId = instructorId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Course>().HasData(courses);

            // Seed Course-Category relationships
            var courseCategories = new List<object>
            {
                new { CoursesId = csharpCourseId, CategoriesId = programmingCategoryId },
                new { CoursesId = webDevCourseId, CategoriesId = webDevCategoryId },
                new { CoursesId = dataScienceCourseId, CategoriesId = dataScienceCategoryId },
                new { CoursesId = reactCourseId, CategoriesId = frontendCategoryId },
                new { CoursesId = nodeJsCourseId, CategoriesId = backendCategoryId },
                new { CoursesId = pythonCourseId, CategoriesId = dataScienceCategoryId },
                new { CoursesId = flutterCourseId, CategoriesId = mobileDevCategoryId },
                new { CoursesId = machineLearningCourseId, CategoriesId = machineLearningCategoryId }
            };
            modelBuilder.Entity<Course>().HasMany(c => c.Categories).WithMany(c => c.Courses).UsingEntity(j => j.HasData(courseCategories));

            // Seed Modules
            var moduleId = Guid.NewGuid();
            var modules = new List<Module>
            {
                new Module
                {
                    Id = moduleId,
                    Title = "Getting Started with C#",
                    Description = "Learn the basics of C# syntax and programming concepts",
                    Order = 1,
                    CourseId = csharpCourseId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Module>().HasData(modules);

            // Seed Lessons
            var lessonId = Guid.NewGuid();
            var lessons = new List<Lesson>
            {
                new Lesson
                {
                    Id = lessonId,
                    Title = "Variables and Data Types",
                    Description = "Understanding variables and data types in C#",
                    Content = "In this lesson, we'll learn about variables and data types...",
                    Order = 1,
                    DurationInMinutes = 30,
                    ModuleId = moduleId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Lesson>().HasData(lessons);

            // Seed Quizzes
            var quizId = Guid.NewGuid();
            var quizzes = new List<Quiz>
            {
                new Quiz
                {
                    Id = quizId,
                    Title = "C# Basics Quiz",
                    Description = "Test your knowledge of C# basics",
                    TimeLimitInMinutes = 30,
                    PassingScore = 70,
                    IsPublished = true,
                    ModuleId = moduleId,
                    InstructorId = instructorId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Quiz>().HasData(quizzes);

            // Seed Questions
            var questionId = Guid.NewGuid();
            var questions = new List<Question>
            {
                new Question
                {
                    Id = questionId,
                    Text = "What is a variable in C#?",
                    Points = 10,
                    QuizId = quizId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Question>().HasData(questions);

            // Seed Options
            var options = new List<Option>
            {
                new Option
                {
                    Id = Guid.NewGuid(),
                    Text = "A container for storing data",
                    IsCorrect = true,
                    QuestionId = questionId,
                    CreatedAt = DateTime.UtcNow
                },
                new Option
                {
                    Id = Guid.NewGuid(),
                    Text = "A method in C#",
                    IsCorrect = false,
                    QuestionId = questionId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Option>().HasData(options);

            // Seed Enrollments
            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentId = studentId,
                    CourseId = csharpCourseId,
                    EnrolledAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Enrollment>().HasData(enrollments);
        }
    }
} 