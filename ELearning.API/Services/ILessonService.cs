using ELearning.Core.Domain;

namespace ELearning.API.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllLessonsAsync();
        Task<Lesson?> GetLessonByIdAsync(Guid id);
        Task<Lesson> CreateLessonAsync(Lesson lesson);
        Task<Lesson> UpdateLessonAsync(Lesson lesson);
        Task DeleteLessonAsync(Guid id);
        Task<IEnumerable<Lesson>> GetModuleLessonsAsync(Guid moduleId);
        Task<Progress> MarkLessonAsCompletedAsync(Guid lessonId, Guid studentId);
        Task<Progress> UpdateLessonProgressAsync(Guid lessonId, Guid studentId, int timeSpent);
    }
} 
 