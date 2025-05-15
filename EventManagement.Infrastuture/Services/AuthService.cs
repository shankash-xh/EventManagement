using EventManagement.Application.Interface;
using EventManagement.Application.Request.User;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventManagement.Infrastuture.Services;

public class AuthService(IHttpContextAccessor contextAccessor, IConfiguration configuration, UserManager<User> userManager) : IAuthService
{
    private readonly IHttpContextAccessor? _contextAccessor = contextAccessor;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Result<UserResponce>> LoginAsync(LoginUserRequest request)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null) { return Result<UserResponce>.Failure("Incorect UserName or Password"); }
        bool validPassword = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!validPassword) { return Result<UserResponce>.Failure("Incorect UserName or Password"); }
        IList<string> roles = await _userManager.GetRolesAsync(user);
        string token = CreateAccessToken(user, roles);
        string refreshToken = CreateRefreshToken();
        user.RefeshToken = refreshToken;
        user.RefeshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);
        UserResponce? userResponce = new()
        {
            Token = token,
            RefeshToken = refreshToken,
        };
        return Result<UserResponce>.Success(userResponce);
    }
    private static string CreateRefreshToken()
    {
        byte[]? randomNumber = new byte[32];
        using RandomNumberGenerator? rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
    }
    private string CreateAccessToken(User user, IList<string> userRoles)
    {
        List<Claim>? claims =
        [
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        ];
        foreach (string role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        SigningCredentials? creds = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken? token = new(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );
        JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
        string? tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }


    public async Task<bool> LogoutAsync()
    {
        string? userEmail = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        Console.WriteLine($"User logged out: {userEmail}");
        User? user = await _userManager.FindByEmailAsync(userEmail!);
        if (user == null) { return false; }
        user.RefeshToken = null;
        user.RefeshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);
        return true;
    }

    public async Task<Result<UserResponce>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        ClaimsPrincipal? principal = GetPrincipalFromToken(request.Token!);
        if (principal == null) { return null!; }
        string? userEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        User? user = await _userManager.FindByEmailAsync(userEmail!);
        if (user == null) { return null!; }
        if (user.RefeshToken != request.RefreshToken || user.RefeshTokenExpiryTime <= DateTime.UtcNow)
        {
            return Result<UserResponce>.Failure("Failed To Varify the token");
        }
        string newAccessToken = CreateAccessToken(user, await _userManager.GetRolesAsync(user));
        string newRefreshToken = CreateRefreshToken();
        user.RefeshToken = newRefreshToken;
        user.RefeshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);
        UserResponce? userResponce = new()
        {
            Token = newAccessToken,
            RefeshToken = newRefreshToken,
        };
        return Result<UserResponce>.Success(userResponce);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string accesToken)
    {
        try
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                ValidateIssuer = false,
                //ValidIssuer = _configuration["JWT:Issuer"],
                ValidateAudience = false,
                //ValidAudience = _configuration["JWT:Audience"],
                ValidateLifetime = true,
            };
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(accesToken, tokenValidationParameters, out SecurityToken validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
