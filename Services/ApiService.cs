using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                BaseAddress = new Uri("http://apipos.somee.com/")
            };
        }

        public async Task InitializeTokenAsync()
        {
            _accessToken = await TokenStorage.GetAccessTokenAsync();
        }

        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }

        public async Task<T> PostAsync<T>(string url, object data, bool requireAuth = false)
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
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> GetAsync<T>(string url, bool requireAuth = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (requireAuth && !string.IsNullOrEmpty(_accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
