using ELearning.Core.Domain;
using ELearning.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllLessons()
        {
            var lessons = await _lessonService.GetAllLessonsAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLessonById(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return Ok(lesson);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Lesson>> CreateLesson(Lesson lesson)
        {
            var createdLesson = await _lessonService.CreateLessonAsync(lesson);
            return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Lesson>> UpdateLesson(Guid id, Lesson lesson)
        {
            try
            {
                lesson.Id = id;
                var updatedLesson = await _lessonService.UpdateLessonAsync(lesson);
                return Ok(updatedLesson);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            try
            {
                await _lessonService.DeleteLessonAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("module/{moduleId}")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetModuleLessons(Guid moduleId)
        {
            var lessons = await _lessonService.GetModuleLessonsAsync(moduleId);
            return Ok(lessons);
        }

        [HttpPost("{lessonId}/complete/{studentId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Progress>> MarkLessonAsCompleted(Guid lessonId, Guid studentId)
        {
            try
            {
                var progress = await _lessonService.MarkLessonAsCompletedAsync(lessonId, studentId);
                return Ok(progress);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{lessonId}/progress/{studentId}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Progress>> UpdateLessonProgress(Guid lessonId, Guid studentId, [FromBody] int timeSpent)
        {
            try
            {
                var progress = await _lessonService.UpdateLessonProgressAsync(lessonId, studentId, timeSpent);
                return Ok(progress);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
} 
 