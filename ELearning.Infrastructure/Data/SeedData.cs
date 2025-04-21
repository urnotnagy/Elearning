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
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Programming", Description = "Learn programming languages and software development" },
                new Category { Id = Guid.NewGuid(), Name = "Web Development", Description = "Learn web development technologies" },
                new Category { Id = Guid.NewGuid(), Name = "Data Science", Description = "Learn data science and analytics" }
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
            var courseId = Guid.NewGuid();
            var courses = new List<Course>
            {
                new Course
                {
                    Id = courseId,
                    Title = "Introduction to C#",
                    Description = "Learn the fundamentals of C# programming",
                    Price = 49.99m,
                    IsPublished = true,
                    InstructorId = instructorId,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Course>().HasData(courses);

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
                    CourseId = courseId,
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
                    CourseId = courseId,
                    EnrolledAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Enrollment>().HasData(enrollments);
        }
    }
} 