using MauiAppUIDemo.Services;
using MauiAppUIDemo.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiAppUIDemo
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;

            MainPage = new LoadingPage(); // Có thể đổi thành splash UI tạm

            InitRootPage(serviceProvider);
        }

        private async void InitRootPage(IServiceProvider services)
        {
            var api = services.GetRequiredService<IApiService>();
            var token = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                api.SetAccessToken(token);
                var shell = Services.GetRequiredService<AppShell>();
                MainPage = shell;

                // ✅ Sau khi MainPage đã được gán, Shell.Current mới hoạt động
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await shell.InitNavigationAsync();
                });
            }
            else
            {
                var loginPage = services.GetRequiredService<LoginPage>();
                MainPage = new NavigationPage(loginPage);
            }
        }
    }


}



