using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ecommerce.DTOs.User;
using Ecommerce.DTOs.Auth;
using Ecommerce.Model;
using Ecommerce.Services.Interfaces;
using Ecommerce.Common.ServiceResult;

namespace Ecommerce.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    // REGISTER PUBLIC: bisa dipanggil controller
    public async Task<ServiceResult<AuthResponseDto>> RegisterAsync(UserRegisterDto dto)
    {
        // cek user, create, validate password, dll
        var user = _mapper.Map<User>(dto);

        user.UserName = string.IsNullOrWhiteSpace(dto.FullName)
        ? dto.Email.Split('@')[0] // fallback kalau FullName kosong
        : new string(dto.FullName.Where(char.IsLetterOrDigit).ToArray());
        
        var result = await 
        _userManager.CreateAsync(user, dto.Password);


        if (!result.Succeeded)
            return ServiceResult<AuthResponseDto>.ErrorResult(
                string.Join(", ", result.Errors.Select(e => e.Description))
            );

        var roleResult = await _userManager.AddToRoleAsync(user, "Seller");

        if (!roleResult.Succeeded)
            return ServiceResult<AuthResponseDto>.ErrorResult("Gagal assign role");

        // generate token dengan method private
        var token = await GenerateJwtToken(user);


        return ServiceResult<AuthResponseDto>.SuccessResult(token, "Registrasi berhasil");
    }

    // LOGIN PUBLIC: bisa dipanggil controller
    public async Task<ServiceResult<AuthResponseDto>> LoginAsync(UserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) 
            return ServiceResult<AuthResponseDto>.ErrorResult("Email atau password salah");

        var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!check.Succeeded) 
            return ServiceResult<AuthResponseDto>.ErrorResult("Email atau password salah");

        var token = await GenerateJwtToken(user);
        return ServiceResult<AuthResponseDto>.SuccessResult(token, "Login berhasil");
    }

    // PRIVATE: internal helper untuk bikin JWT
    private async Task<AuthResponseDto> GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var userDto = _mapper.Map<UserResponseDto>(user);
            userDto.Roles = roles.ToList();

        return new AuthResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = tokenDescriptor.Expires.Value,
            User = userDto
        };
    }

    // GET CURRENT USER
    public async Task<ServiceResult<UserResponseDto>> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return ServiceResult<UserResponseDto>.ErrorResult("User tidak ditemukan");

        var userDto = _mapper.Map<UserResponseDto>(user);
        var roles = await _userManager.GetRolesAsync(user);
        userDto.Roles = roles.ToList();


        return ServiceResult<UserResponseDto>.SuccessResult(userDto);
    }

}
