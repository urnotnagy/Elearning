using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.User
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Name => $"{FirstName} {LastName}";
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int InstructedCoursesCount { get; set; }
        public int EnrolledCoursesCount { get; set; }
    }
} 
 
