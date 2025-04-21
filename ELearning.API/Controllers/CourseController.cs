using ELearning.Core.Domain;
using ELearning.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ELearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            var instructorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            course.InstructorId = instructorId;
            var createdCourse = await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Course>> UpdateCourse(Guid id, Course course)
        {
            try
            {
                course.Id = id;
                var updatedCourse = await _courseService.UpdateCourseAsync(course);
                return Ok(updatedCourse);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{courseId}/enroll/{studentId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnrollStudent(Guid courseId, Guid studentId)
        {
            try
            {
                await _courseService.EnrollStudentAsync(courseId, studentId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{courseId}/enroll/{studentId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UnenrollStudent(Guid courseId, Guid studentId)
        {
            try
            {
                await _courseService.UnenrollStudentAsync(courseId, studentId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{courseId}/reviews")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Review>> AddReview(Guid courseId, [FromBody] Review review)
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                review.StudentId = studentId;
                review.CourseId = courseId;
                var createdReview = await _courseService.AddReviewAsync(review);
                return Ok(createdReview);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetInstructorCourses(Guid instructorId)
        {
            var courses = await _courseService.GetInstructorCoursesAsync(instructorId);
            return Ok(courses);
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<Course>>> GetStudentCourses(Guid studentId)
        {
            var courses = await _courseService.GetStudentCoursesAsync(studentId);
            return Ok(courses);
        }
    }
} 
 