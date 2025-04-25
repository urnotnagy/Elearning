using ELearning.API.DTOs.Course;
using ELearning.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ELearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all courses");
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(Guid id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting course with ID {CourseId}", id);
                return StatusCode(500, "An error occurred while retrieving the course");
            }
        }

        [HttpPost]
        [Authorize(Policy = "RequireInstructorRole")]
        public async Task<ActionResult<CourseDto>> Create(CreateCourseDto dto)
        {
            try
            {
                var instructorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var course = await _courseService.CreateCourseAsync(dto, instructorId);
                return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course");
                return StatusCode(500, "An error occurred while creating the course");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireInstructorRole")]
        public async Task<ActionResult<CourseDto>> Update(Guid id, UpdateCourseDto dto)
        {
            try
            {
                var instructorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var existingCourse = await _courseService.GetCourseByIdAsync(id);
                
                if (existingCourse == null)
                {
                    return NotFound($"Course with ID {id} not found");
                }

                if (existingCourse.InstructorId != instructorId)
                {
                    return Forbid();
                }

                var course = await _courseService.UpdateCourseAsync(id, dto);
                return Ok(course);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course with ID {CourseId}", id);
                return StatusCode(500, "An error occurred while updating the course");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole,RequireInstructorRole")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                var existingCourse = await _courseService.GetCourseByIdAsync(id);
                
                if (existingCourse == null)
                {
                    return NotFound($"Course with ID {id} not found");
                }

                // Allow admins to delete any course, instructors can only delete their own
                if (userRole != "Admin" && existingCourse.InstructorId != userId)
                {
                    return Forbid();
                }

                await _courseService.DeleteCourseAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course with ID {CourseId}", id);
                return StatusCode(500, "An error occurred while deleting the course");
            }
        }

        [HttpPost("{courseId}/enroll")]
        [Authorize(Policy = "RequireStudentRole")]
        public async Task<IActionResult> Enroll(Guid courseId)
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                await _courseService.EnrollStudentAsync(courseId, studentId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enrolling student in course with ID {CourseId}", courseId);
                return StatusCode(500, "An error occurred while enrolling in the course");
            }
        }

        [HttpDelete("{courseId}/enroll")]
        [Authorize(Policy = "RequireStudentRole")]
        public async Task<IActionResult> Unenroll(Guid courseId)
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                await _courseService.UnenrollStudentAsync(courseId, studentId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unenrolling student from course with ID {CourseId}", courseId);
                return StatusCode(500, "An error occurred while unenrolling from the course");
            }
        }

        [HttpGet("instructor")]
        [Authorize(Policy = "RequireInstructorRole")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetInstructorCourses()
        {
            try
            {
                var instructorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var courses = await _courseService.GetInstructorCoursesAsync(instructorId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting instructor courses");
                return StatusCode(500, "An error occurred while retrieving instructor courses");
            }
        }

        [HttpGet("student")]
        [Authorize(Policy = "RequireStudentRole")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetStudentCourses()
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var courses = await _courseService.GetStudentCoursesAsync(studentId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student courses");
                return StatusCode(500, "An error occurred while retrieving student courses");
            }
        }
    }
} 
 