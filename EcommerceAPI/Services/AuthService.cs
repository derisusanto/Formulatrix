using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ecommerce.DTOs.user;
using Ecommerce.Models;
using Ecommerce.Common;

namespace Ecommerce.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    // REGISTER
    public async Task<ServiceResult<UserResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        // cek email sudah ada
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return ServiceResult<UserResponseDto>.ErrorResult("User dengan email ini sudah ada");

        // map dto ke entity
        var user = _mapper.Map<User>(registerDto);

        // create user dengan password
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return ServiceResult<UserDto>.ErrorResult($"Registrasi gagal: {errors}");
        }

        var userDto = _mapper.Map<UserResponseDto>(user);
        return ServiceResult<UserResponseDto>.SuccessResult(userDto, "Registrasi berhasil");
    }

    // LOGIN
    public async Task<ServiceResult<UserResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return ServiceResult<UserResponseDto>.ErrorResult("Email atau password salah");

        var check = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!check)
            return ServiceResult<UserResponseDto>.ErrorResult("Email atau password salah");

        var userDto = _mapper.Map<UserResponseDto>(user);
        return ServiceResult<UserResponseDto>.SuccessResult(userDto, "Login berhasil");
    }
}
