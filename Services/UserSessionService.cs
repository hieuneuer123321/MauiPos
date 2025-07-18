using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public static class UserSessionService
    {
        private const string UserKey = "current_user";

        public static async Task SaveUserAsync(object user)
        {
            var json = JsonSerializer.Serialize(user);
            Preferences.Set(UserKey, json);
        }

        public static T? GetUser<T>()
        {
            var json = Preferences.Get(UserKey, null);
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }

        public static void ClearUser()
        {
            Preferences.Remove(UserKey);
        }
    }
}
