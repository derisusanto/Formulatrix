
namespace Ecommerce.Service.Interface;
public interface IAuthService
{
    Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    Task<ServiceResult<UserDto>> GetCurrentUserAsync(string userId);
}
