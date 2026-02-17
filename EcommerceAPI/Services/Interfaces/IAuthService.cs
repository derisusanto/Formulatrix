using Ecommerce.DTOs.Auth;
using Ecommerce.DTOs.User;
using Ecommerce.Common.ServiceResult;

namespace Ecommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResponseDto>> RegisterAsync(UserRegisterDto dto);
        Task<ServiceResult<AuthResponseDto>> LoginAsync(UserLoginDto dto);
        Task<ServiceResult<UserResponseDto>> GetCurrentUserAsync(string userId);
    }
}
