using EventManagement.Application.Interface;
using EventManagement.Application.Request.User;
using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<Result<UserResponce>> Login([FromBody] LoginUserRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return result;
    }

    [HttpPost("refresh-token")]
    public async Task<Result<UserResponce>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var result = await _authService.RefreshTokenAsync(refreshTokenRequest);
        return result;
    }

    [HttpPost("logout")]
    public async Task<Result<string>> Logout()
    {
        var result = await _authService.LogoutAsync();
        return result ? Result<string>.Success("Loged Out"): Result<string>.Failure("Failed to Login");
    }
}
