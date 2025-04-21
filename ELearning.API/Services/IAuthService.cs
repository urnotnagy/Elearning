using ELearning.API.DTOs.Auth;
using ELearning.API.DTOs.User;
using ELearning.Core.Domain;

namespace ELearning.API.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> UpdateUserAsync(Guid id, UpdateProfileDto updateProfileDto);
        Task<bool> ChangePasswordAsync(Guid id, string currentPassword, string newPassword);
        Task<bool> IsEmailUniqueAsync(string email);
    }
} 
 