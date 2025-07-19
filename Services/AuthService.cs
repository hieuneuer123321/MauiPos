using MauiAppUIDemo.Models;
using System;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        void SetToken(string accessToken);
        Task<string> GetAccessTokenAsync();
        Task<bool> ReloadByRefreshToken();
        Task LogoutAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var payload = new
            {
                userName = email, // Backend yêu cầu key là "userName"
                password
            };

            var result = await _apiService.PostAsync<LoginResponse>(
                "Authorization/LoginAuthorizationJWT",
                payload,
                requireAuth: false);

            if (result.Succeeded)
            {
                await TokenStorage.SaveTokenAsync(
                    result.Data.AccessToken,
                    result.Data.RefreshToken,
                    result.Data.ExpiresIn);

                _apiService.SetAccessToken(result.Data.AccessToken);
            }

            return result;
        }

        public void SetToken(string accessToken)
        {
            _apiService.SetAccessToken(accessToken);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await TokenStorage.GetAccessTokenAsync();
        }

        public async Task<bool> ReloadByRefreshToken()
        {
            var refreshToken = await TokenStorage.GetRefreshTokenAsync();
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            try
            {
                var payload = new { refreshToken };
                var result = await _apiService.PostAsync<LoginResponse>("Authorization/ReloadByRefreshToken", payload, requireAuth: false);

                if (result.Succeeded)
                {
                    await TokenStorage.SaveTokenAsync(result.Data.AccessToken, result.Data.RefreshToken, result.Data.ExpiresIn);
                    return true;
                }

                 TokenStorage.ClearTokens();
                return false;
            }
            catch
            {
                 TokenStorage.ClearTokens();
                return false;
            }
        }


        public async Task LogoutAsync()
        {
             TokenStorage.ClearTokens();
            _apiService.SetAccessToken(null);
        }
    }
}
