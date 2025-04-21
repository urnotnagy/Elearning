using ELearning.Core.Domain;

namespace ELearning.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<Course> CreateCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Guid id);
        Task EnrollStudentAsync(Guid courseId, Guid studentId);
        Task UnenrollStudentAsync(Guid courseId, Guid studentId);
        Task<Review> AddReviewAsync(Review review);
        Task<IEnumerable<Course>> GetInstructorCoursesAsync(Guid instructorId);
        Task<IEnumerable<Course>> GetStudentCoursesAsync(Guid studentId);
    }
} 
 
