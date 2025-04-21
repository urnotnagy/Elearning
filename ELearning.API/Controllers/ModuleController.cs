using ELearning.Core.Domain;
using ELearning.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            var modules = await _moduleService.GetAllModulesAsync();
            return Ok(modules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModuleById(Guid id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Module>> CreateModule(Module module)
        {
            var createdModule = await _moduleService.CreateModuleAsync(module);
            return CreatedAtAction(nameof(GetModuleById), new { id = createdModule.Id }, createdModule);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<Module>> UpdateModule(Guid id, Module module)
        {
            try
            {
                module.Id = id;
                var updatedModule = await _moduleService.UpdateModuleAsync(module);
                return Ok(updatedModule);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteModule(Guid id)
        {
            try
            {
                await _moduleService.DeleteModuleAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<Module>>> GetCourseModules(Guid courseId)
        {
            var modules = await _moduleService.GetCourseModulesAsync(courseId);
            return Ok(modules);
        }
    }
} 
 