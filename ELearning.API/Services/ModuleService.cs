using ELearning.Core.Domain;
using ELearning.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ELearning.API.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<Course> _courseRepository;

        public ModuleService(
            IRepository<Module> moduleRepository,
            IRepository<Course> courseRepository)
        {
            _moduleRepository = moduleRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            return await _moduleRepository.GetAll()
                .Include(m => m.Course)
                .Include(m => m.Lessons)
                .Include(m => m.Assignments)
                .Include(m => m.Quizzes)
                .Include(m => m.Resources)
                .ToListAsync();
        }

        public async Task<Module?> GetModuleByIdAsync(Guid id)
        {
            return await _moduleRepository.GetAll()
                .Include(m => m.Course)
                .Include(m => m.Lessons)
                .Include(m => m.Assignments)
                .Include(m => m.Quizzes)
                .Include(m => m.Resources)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Module> CreateModuleAsync(Module module)
        {
            var course = await _courseRepository.GetByIdAsync(module.CourseId);
            if (course == null)
            {
                throw new InvalidOperationException("Course not found");
            }

            await _moduleRepository.AddAsync(module);
            await _moduleRepository.SaveChangesAsync();
            return module;
        }

        public async Task<Module> UpdateModuleAsync(Module module)
        {
            var existingModule = await _moduleRepository.GetByIdAsync(module.Id);
            if (existingModule == null)
            {
                throw new InvalidOperationException("Module not found");
            }

            existingModule.Title = module.Title;
            existingModule.Description = module.Description;
            existingModule.Order = module.Order;

            await _moduleRepository.UpdateAsync(existingModule);
            await _moduleRepository.SaveChangesAsync();
            return existingModule;
        }

        public async Task DeleteModuleAsync(Guid id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            if (module == null)
            {
                throw new InvalidOperationException("Module not found");
            }

            await _moduleRepository.DeleteAsync(module);
            await _moduleRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Module>> GetCourseModulesAsync(Guid courseId)
        {
            return await _moduleRepository.GetAll()
                .Where(m => m.CourseId == courseId)
                .Include(m => m.Lessons)
                .Include(m => m.Assignments)
                .Include(m => m.Quizzes)
                .Include(m => m.Resources)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }
    }
} 
 