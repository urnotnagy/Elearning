using ELearning.API.DTOs;

namespace ELearning.API.DTOs.User
{
    public class UpdateProfileDto : BaseUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
} 
 