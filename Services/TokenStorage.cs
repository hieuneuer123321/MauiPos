using Microsoft.Maui.Storage;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public static class TokenStorage
    {
        private const string AccessTokenKey = "access_token";
        private const string RefreshTokenKey = "refresh_token";

        public static async Task SaveTokensAsync(string accessToken, string refreshToken)
        {
            await SecureStorage.SetAsync(AccessTokenKey, accessToken);
            await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);
        }

        public static async Task<string> GetAccessTokenAsync()
        {
            return await SecureStorage.GetAsync(AccessTokenKey);
        }

        public static async Task<string> GetRefreshTokenAsync()
        {
            return await SecureStorage.GetAsync(RefreshTokenKey);
        }

        public static void RemoveTokens()
        {
            SecureStorage.Remove(AccessTokenKey);
            SecureStorage.Remove(RefreshTokenKey);
        }
    }
}
