using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public interface IApiService
    {
        Task<T> PostAsync<T>(string url, object data, bool requireAuth = false);
        Task<T> GetAsync<T>(string url, bool requireAuth = false);
        void SetAccessToken(string token);

        Task InitializeTokenAsync();
    }
}
