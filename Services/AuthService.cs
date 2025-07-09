using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        void SetToken(string accessToken); // ✅ thêm hàm này
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
                userName = email, // <-- Bắt buộc dùng key userName như Postman
                password
            };
            var result = await _apiService.PostAsync<LoginResponse>("Authorization/LoginAuthorizationJWT", payload, requireAuth: false);

            if (result.Succeeded)
                _apiService.SetAccessToken(result.Data.AccessToken);

            return result;
        }
        public void SetToken(string accessToken)
        {
            _apiService.SetAccessToken(accessToken);
        }
    }


}
