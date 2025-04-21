using ELearning.API.DTOs.User;
using ELearning.Core.Domain;

namespace ELearning.API.DTOs.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
} 
 
