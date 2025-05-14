using EventManagement.Application.Request.User;
using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;

namespace EventManagement.Application.Interface;

public interface IAuthService
{

    Task<Result<UserResponce>> LoginAsync(LoginUserRequest request);
    Task<Result<UserResponce>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    Task<bool> LogoutAsync();

}

