using ELearning.API.DTOs.Course;
using ELearning.Core.Domain;
using ELearning.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ELearning.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;

        public CourseService(
            IRepository<Course> courseRepository,
            IRepository<User> userRepository,
            IRepository<Review> reviewRepository,
            IRepository<Enrollment> enrollmentRepository,
            IRepository<Category> categoryRepository,
            IMapper mapper,
            ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _enrollmentRepository = enrollmentRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Include(c => c.Enrollments)
                .Include(c => c.Reviews)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetCourseByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Include(c => c.Enrollments)
                .Include(c => c.Reviews)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found", id);
                return null;
            }

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto, Guid instructorId)
        {
            var instructor = await _userRepository.GetByIdAsync(instructorId);
            if (instructor == null)
            {
                _logger.LogWarning("Instructor with ID {InstructorId} not found", instructorId);
                throw new ArgumentException("Instructor not found", nameof(instructorId));
            }

            var course = _mapper.Map<Course>(dto);
            course.InstructorId = instructorId;
            course.CreatedAt = DateTime.UtcNow;

            if (dto.CategoryIds?.Any() == true)
            {
                var categories = await _categoryRepository.GetAll()
                    .Where(c => dto.CategoryIds.Contains(c.Id))
                    .ToListAsync();
                course.Categories.Clear();
                foreach (var category in categories)
                {
                    course.Categories.Add(category);
                }
            }

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Created new course with ID {CourseId}", course.Id);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetAll()
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found for update", id);
                throw new KeyNotFoundException($"Course with ID {id} not found");
            }

            _mapper.Map(dto, course);
            course.UpdatedAt = DateTime.UtcNow;

            if (dto.CategoryIds != null)
            {
                course.Categories.Clear();
                var categories = await _categoryRepository.GetAll()
                    .Where(c => dto.CategoryIds.Contains(c.Id))
                    .ToListAsync();
                foreach (var category in categories)
                {
                    course.Categories.Add(category);
                }
            }

            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Updated course with ID {CourseId}", id);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException($"Course {id} not found");

            await _courseRepository.DeleteAsync(course);
            await _courseRepository.SaveChangesAsync();
            _logger.LogInformation("Deleted course with ID {CourseId}", id);
        }

        public async Task EnrollStudentAsync(Guid courseId, Guid studentId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            var student = await _userRepository.GetByIdAsync(studentId);

            if (course == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found", courseId);
                throw new ArgumentException("Course not found", nameof(courseId));
            }
            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found", studentId);
                throw new ArgumentException("Student not found", nameof(studentId));
            }

            var existing = await _enrollmentRepository.GetAll()
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);
            if (existing != null)
            {
                _logger.LogWarning("Student {StudentId} already enrolled in course {CourseId}", studentId, courseId);
                throw new InvalidOperationException("Student is already enrolled in this course");
            }

            var enrollment = new Enrollment { CourseId = courseId, StudentId = studentId, EnrolledAt = DateTime.UtcNow };
            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
            _logger.LogInformation("Enrolled student {StudentId} in course {CourseId}", studentId, courseId);
        }

        public async Task UnenrollStudentAsync(Guid courseId, Guid studentId)
        {
            var enrollment = await _enrollmentRepository.GetAll()
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);
            if (enrollment == null)
                throw new ArgumentException("Enrollment not found");

            await _enrollmentRepository.DeleteAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
            _logger.LogInformation("Unenrolled student {StudentId} from course {CourseId}", studentId, courseId);
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            var course = await _courseRepository.GetByIdAsync(review.CourseId);
            var student = await _userRepository.GetByIdAsync(review.StudentId);
            if (course == null)
                throw new ArgumentException("Course not found", nameof(review.CourseId));
            if (student == null)
                throw new ArgumentException("Student not found", nameof(review.StudentId));

            var enrollment = await _enrollmentRepository.GetAll()
                .FirstOrDefaultAsync(e => e.CourseId == review.CourseId && e.StudentId == review.StudentId);
            if (enrollment == null)
                throw new InvalidOperationException("Student is not enrolled in this course");

            var existing = await _reviewRepository.GetAll()
                .FirstOrDefaultAsync(r => r.CourseId == review.CourseId && r.StudentId == review.StudentId);
            if (existing != null)
                throw new InvalidOperationException("Student has already reviewed this course");

            review.CreatedAt = DateTime.UtcNow;
            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
            _logger.LogInformation("Added review for course {CourseId} by student {StudentId}", review.CourseId, review.StudentId);
            return review;
        }

        public async Task<IEnumerable<CourseDto>> GetInstructorCoursesAsync(Guid instructorId)
        {
            var courses = await _courseRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Include(c => c.Reviews)
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<IEnumerable<CourseDto>> GetStudentCoursesAsync(Guid studentId)
        {
            var courseIds = await _enrollmentRepository.GetAll()
                .Where(e => e.StudentId == studentId)
                .Select(e => e.CourseId)
                .ToListAsync();

            var courses = await _courseRepository.GetAll()
                .AsNoTracking()
                .Include(c => c.Instructor)
                .Include(c => c.Categories)
                .Include(c => c.Reviews)
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();

            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
    }
}
