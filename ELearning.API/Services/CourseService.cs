using ELearning.Core.Domain;
using ELearning.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ELearning.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;

        public CourseService(
            IRepository<Course> courseRepository,
            IRepository<User> userRepository,
            IRepository<Review> reviewRepository,
            IRepository<Enrollment> enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAll()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepository.GetAll()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
            return course;
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(course.Id);
            if (existingCourse == null)
            {
                throw new ArgumentException("Course not found", nameof(course));
            }

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Price = course.Price;
            existingCourse.Level = course.Level;
            existingCourse.UpdatedAt = DateTime.UtcNow;

            // Update categories if provided
            if (course.Categories != null && course.Categories.Any())
            {
                existingCourse.Categories.Clear();
                foreach (var category in course.Categories)
                {
                    existingCourse.Categories.Add(category);
                }
            }

            await _courseRepository.UpdateAsync(existingCourse);
            await _courseRepository.SaveChangesAsync();

            return existingCourse;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new ArgumentException("Course not found", nameof(id));
            }

            await _courseRepository.DeleteAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task EnrollStudentAsync(Guid courseId, Guid studentId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            var student = await _userRepository.GetByIdAsync(studentId);

            if (course == null)
                throw new ArgumentException("Course not found", nameof(courseId));
            if (student == null)
                throw new ArgumentException("Student not found", nameof(studentId));

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                EnrolledAt = DateTime.UtcNow
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }

        public async Task UnenrollStudentAsync(Guid courseId, Guid studentId)
        {
            var enrollment = await _enrollmentRepository.GetAll()
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            if (enrollment == null)
                throw new ArgumentException("Enrollment not found");

            await _enrollmentRepository.DeleteAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            var course = await _courseRepository.GetByIdAsync(review.CourseId);
            var student = await _userRepository.GetByIdAsync(review.StudentId);

            if (course == null)
                throw new ArgumentException("Course not found", nameof(review.CourseId));
            if (student == null)
                throw new ArgumentException("Student not found", nameof(review.StudentId));

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Course>> GetInstructorCoursesAsync(Guid instructorId)
        {
            return await _courseRepository.GetAll()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(Guid studentId)
        {
            return await _courseRepository.GetAll()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Include(c => c.Enrollments)
                .Where(c => c.Enrollments.Any(e => e.StudentId == studentId))
                .ToListAsync();
        }
    }
} 