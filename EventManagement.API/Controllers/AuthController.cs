using EventManagement.Application.Interface;
using EventManagement.Application.Request.User;
using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IValidator<LoginUserRequest> validation) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<LoginUserRequest> _validation = validation;

    [HttpPost("login")]
    public async Task<Result<UserResponce>> Login([FromBody] LoginUserRequest request)
    {
        ValidationResult? validationResult = await _validation.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            List<string>? errors = [.. validationResult.Errors.Select(e => e.ErrorMessage)];
            return Result<UserResponce>.Failure(string.Join(", ", errors));
            //return Result<UserResponce>.Failure("Validation Failed");
        }
        Result<UserResponce>? result = await _authService.LoginAsync(request);
        return result;
    }

    [HttpPost("refresh-token")]
    public async Task<Result<UserResponce>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        Result<UserResponce>? result = await _authService.RefreshTokenAsync(refreshTokenRequest);
        return result;
    }

    [HttpPost("logout")]
    public async Task<Result<string>> Logout()
    {
        var result = await _authService.LogoutAsync();
        return result ? Result<string>.Success("Loged Out") : Result<string>.Failure("Failed to Login");
    }
}
