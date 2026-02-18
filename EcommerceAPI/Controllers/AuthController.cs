using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Ecommerce.DTOs.User;
using Ecommerce.Services.Interfaces;
using Ecommerce.Common.ServiceResult;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UserRegisterDto> _registerValidator;
        private readonly IValidator<UserLoginDto> _loginValidator;

        public AuthController(
            IAuthService authService,
            IValidator<UserRegisterDto> registerValidator,
            IValidator<UserLoginDto> loginValidator)
        {
            _authService = authService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>ServiceResult with user info</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            // Validasi input DTO
            var validationResult = await _registerValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var ErrorResponse = ServiceResult<UserResponseDto>.ErrorResult("Validation failed: " + string.Join(", ", errors));
                return BadRequest(ErrorResponse);
            }

            // Panggil service untuk register
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
                return BadRequest(result); // ServiceResult sudah punya Success, Message, Data

            return Ok(result);
        }

        /// <summary>
        /// Authenticate user and return user info
        /// </summary>
        /// <param name="loginDto">User login data</param>
        /// <returns>ServiceResult with user info</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            // Validasi input DTO
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var ErrorResponse = ServiceResult<UserResponseDto>.ErrorResult("Validation failed: " + string.Join(", ", errors));
                return BadRequest(ErrorResponse);  }

            // Panggil service untuk login
            var result = await _authService.LoginAsync(loginDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Get current logged-in user info
        /// </summary>
        /// <returns>ServiceResult with current user info</returns>
        [HttpGet("loginInformation")]
        [Microsoft.AspNetCore.Authorization.Authorize] // Harus login dulu
        public async Task<IActionResult> GetCurrentUser()
        {
            // Ambil userId dari claim JWT (NameIdentifier)
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(ServiceResult<UserResponseDto>.ErrorResult("User not authorized"));

            // Panggil service untuk ambil user
            var result = await _authService.GetCurrentUserAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
