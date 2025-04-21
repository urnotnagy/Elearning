using ELearning.Core.Common;
using ELearning.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ELearning.Core.Interfaces;

namespace ELearning.API.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Progress> _progressRepository;

        public LessonService(
            IRepository<Lesson> lessonRepository,
            IRepository<Module> moduleRepository,
            IRepository<User> userRepository,
            IRepository<Progress> progressRepository)
        {
            _lessonRepository = lessonRepository;
            _moduleRepository = moduleRepository;
            _userRepository = userRepository;
            _progressRepository = progressRepository;
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return await _lessonRepository.GetAll()
                .Include(l => l.Module)
                .Include(l => l.Resources)
                .Include(l => l.StudentProgress)
                .ToListAsync();
        }

        public async Task<Lesson?> GetLessonByIdAsync(Guid id)
        {
            return await _lessonRepository.GetAll()
                .Include(l => l.Module)
                .Include(l => l.Resources)
                .Include(l => l.StudentProgress)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lesson> CreateLessonAsync(Lesson lesson)
        {
            var module = await _moduleRepository.GetByIdAsync(lesson.ModuleId);
            if (module == null)
            {
                throw new InvalidOperationException("Module not found");
            }

            await _lessonRepository.AddAsync(lesson);
            await _lessonRepository.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            var existingLesson = await _lessonRepository.GetByIdAsync(lesson.Id);
            if (existingLesson == null)
            {
                throw new InvalidOperationException("Lesson not found");
            }

            existingLesson.Title = lesson.Title;
            existingLesson.Description = lesson.Description;
            existingLesson.Content = lesson.Content;
            existingLesson.VideoUrl = lesson.VideoUrl;
            existingLesson.Order = lesson.Order;
            existingLesson.DurationInMinutes = lesson.DurationInMinutes;

            await _lessonRepository.UpdateAsync(existingLesson);
            await _lessonRepository.SaveChangesAsync();
            return existingLesson;
        }

        public async Task DeleteLessonAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                throw new InvalidOperationException("Lesson not found");
            }

            await _lessonRepository.DeleteAsync(lesson);
            await _lessonRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lesson>> GetModuleLessonsAsync(Guid moduleId)
        {
            return await _lessonRepository.GetAll()
                .Where(l => l.ModuleId == moduleId)
                .Include(l => l.Resources)
                .Include(l => l.StudentProgress)
                .OrderBy(l => l.Order)
                .ToListAsync();
        }

        public async Task<Progress> MarkLessonAsCompletedAsync(Guid lessonId, Guid studentId)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            var student = await _userRepository.GetByIdAsync(studentId);

            if (lesson == null)
            {
                throw new InvalidOperationException("Lesson not found");
            }

            if (student == null || student.Role != UserRole.Student)
            {
                throw new InvalidOperationException("Invalid student ID");
            }

            var progress = await _progressRepository.GetAll()
                .FirstOrDefaultAsync(p => p.LessonId == lessonId && p.StudentId == studentId);

            if (progress == null)
            {
                progress = new Progress
                {
                    LessonId = lessonId,
                    StudentId = studentId,
                    IsCompleted = true,
                    CompletedAt = DateTime.UtcNow
                };
                await _progressRepository.AddAsync(progress);
            }
            else
            {
                progress.IsCompleted = true;
                progress.CompletedAt = DateTime.UtcNow;
                await _progressRepository.UpdateAsync(progress);
            }

            await _progressRepository.SaveChangesAsync();
            return progress;
        }

        public async Task<Progress> UpdateLessonProgressAsync(Guid lessonId, Guid studentId, int timeSpent)
        {
            var lesson = await _lessonRepository.GetByIdAsync(lessonId);
            var student = await _userRepository.GetByIdAsync(studentId);

            if (lesson == null)
            {
                throw new InvalidOperationException("Lesson not found");
            }

            if (student == null || student.Role != UserRole.Student)
            {
                throw new InvalidOperationException("Invalid student ID");
            }

            var progress = await _progressRepository.GetAll()
                .FirstOrDefaultAsync(p => p.LessonId == lessonId && p.StudentId == studentId);

            if (progress == null)
            {
                progress = new Progress
                {
                    LessonId = lessonId,
                    StudentId = studentId,
                    TimeSpent = timeSpent
                };
                await _progressRepository.AddAsync(progress);
            }
            else
            {
                progress.TimeSpent += timeSpent;
                await _progressRepository.UpdateAsync(progress);
            }

            await _progressRepository.SaveChangesAsync();
            return progress;
        }
    }
} 