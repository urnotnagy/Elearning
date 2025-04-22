using System.ComponentModel.DataAnnotations;
using ELearning.Core.Domain;
using ELearning.API.DTOs;

namespace ELearning.API.DTOs.User
{
    public class CreateUserDto : BaseCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        public UserRole Role { get; set; } = UserRole.Student;
    }
} 