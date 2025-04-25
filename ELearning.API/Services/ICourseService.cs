using ELearning.API.DTOs.Course;
using ELearning.Core.Domain;

namespace ELearning.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(Guid id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto, Guid instructorId);
        Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseDto dto);
        Task DeleteCourseAsync(Guid id);
        Task EnrollStudentAsync(Guid courseId, Guid studentId);
        Task UnenrollStudentAsync(Guid courseId, Guid studentId);
        Task<Review> AddReviewAsync(Review review);
        Task<IEnumerable<CourseDto>> GetInstructorCoursesAsync(Guid instructorId);
        Task<IEnumerable<CourseDto>> GetStudentCoursesAsync(Guid studentId);
    }
} 
 
