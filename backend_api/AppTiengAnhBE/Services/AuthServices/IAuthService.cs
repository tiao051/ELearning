using AppTiengAnhBE.Models.DTOs.LoginDTO;

namespace AppTiengAnhBE.Services.AuthServices
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(Models.DTOs.LoginDTO.LoginRequest request);
        Task<RefreshTokenResponse> RefreshAccessTokenAsync(RefreshTokenRequest request);
    }
}
