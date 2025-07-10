public static class TokenStorage
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";
    private const string ExpiryKey = "token_expiry"; // Unix timestamp

    public static async Task SaveTokenAsync(string accessToken, string refreshToken, int expiresInSeconds)
    {
        await SecureStorage.SetAsync(AccessTokenKey, accessToken);
        await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);

        var expiryTime = DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds).ToUnixTimeSeconds().ToString();
        await SecureStorage.SetAsync(ExpiryKey, expiryTime);
    }
    public static async Task ClearTokensAsync()
    {
        SecureStorage.Remove(AccessTokenKey);
        SecureStorage.Remove(RefreshTokenKey);
    }
    public static async Task<string> GetAccessTokenAsync()
    {
        return await SecureStorage.GetAsync(AccessTokenKey);
    }

    public static async Task<string> GetRefreshTokenAsync()
    {
        return await SecureStorage.GetAsync(RefreshTokenKey);
    }

    public static async Task<long> GetExpiryTimestampAsync()
    {
        var raw = await SecureStorage.GetAsync(ExpiryKey);
        return long.TryParse(raw, out var ts) ? ts : 0;
    }

    public static void ClearTokens()
    {
        SecureStorage.Remove(AccessTokenKey);
        SecureStorage.Remove(RefreshTokenKey);
        SecureStorage.Remove(ExpiryKey);
    }
}
