using MauiAppUIDemo.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MauiAppUIDemo.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://apipos.somee.com/") // 🔁 Thay bằng API thật
            };
        }

        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }

        public async Task InitializeTokenAsync()
        {
            _accessToken = await TokenStorage.GetAccessTokenAsync();
            Console.WriteLine("🔑 Init token: " + _accessToken); // 👈 log ở đây
        }

        private async Task EnsureTokenValidAsync()
        {
            var expiry = await TokenStorage.GetExpiryTimestampAsync();
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (now >= expiry - 60) // Hết hạn hoặc còn < 1 phút
            {
                var refreshToken = await TokenStorage.GetRefreshTokenAsync();
                if (string.IsNullOrEmpty(refreshToken)) return;

                try
                {
                    var payload = new {
                        refreshToken = refreshToken
                    };
                    var response = await PostAsync<LoginResponse>("Authorization/ReloadByRefreshToken", payload, requireAuth: false);

                    if (response.Succeeded)
                    {
                        _accessToken = response.Data.AccessToken;
                        await TokenStorage.SaveTokenAsync(
                            response.Data.AccessToken,
                            response.Data.RefreshToken,
                            response.Data.ExpiresIn
                        );
                    }
                    else
                    {
                        TokenStorage.ClearTokens(); // Optional: Logout
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Token refresh failed: " + ex.Message);
                }
            }
        }

        public async Task<T> PostAsync<T>(string url, object data, bool requireAuth = false)
        {
            if (requireAuth)
                await EnsureTokenValidAsync();

            try
            {

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                if (requireAuth && !string.IsNullOrEmpty(_accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ API POST Error: " + ex.Message);
                throw;
            }
        }

        public async Task<T> GetAsync<T>(string url, bool requireAuth = false)
        {
            if (requireAuth)
                await EnsureTokenValidAsync();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                if (requireAuth && !string.IsNullOrEmpty(_accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ API GET Error: " + ex.Message);
                throw;
            }
        }
    }
}
