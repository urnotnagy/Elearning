using ELearning.Core.Domain;

namespace ELearning.API.Services
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<Module?> GetModuleByIdAsync(Guid id);
        Task<Module> CreateModuleAsync(Module module);
        Task<Module> UpdateModuleAsync(Module module);
        Task DeleteModuleAsync(Guid id);
        Task<IEnumerable<Module>> GetCourseModulesAsync(Guid courseId);
    }
} 
 
