using ELearning.Core.Domain;

namespace ELearning.API.DTOs.Auth
{
    public class LoginResponseDto
    {
        public ELearning.Core.Domain.User User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
} 
 
