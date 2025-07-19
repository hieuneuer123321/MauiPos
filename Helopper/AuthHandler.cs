
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MauiAppUIDemo.Services;

namespace MauiAppUIDemo.Helopper
{

    public class AuthHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // 👇 Lấy IAuthService thủ công (tránh vòng lặp DI)
                var authService = ServiceHelper.Services.GetRequiredService<IAuthService>();
                var refreshSuccess = await authService.ReloadByRefreshToken();

                if (refreshSuccess)
                {
                    var newAccessToken = await TokenStorage.GetAccessTokenAsync();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                    response.Dispose(); // đóng response cũ
                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("//LoginPage");
                    });

                    return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        RequestMessage = request,
                        ReasonPhrase = "Unauthorized after refresh"
                    };
                }
            }

            return response;
        }
    }


}
