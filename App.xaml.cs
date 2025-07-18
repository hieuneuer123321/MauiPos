using MauiAppUIDemo.Services;
using MauiAppUIDemo.ViewModels;

namespace MauiAppUIDemo
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = new LoadingPage(); // màn hình đợi
            InitRootPage(serviceProvider);
        }

        private async void InitRootPage(IServiceProvider services)
        {
            var api = services.GetRequiredService<IApiService>();
            var token = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                api.SetAccessToken(token);
                MainPage = services.GetRequiredService<AppShell>(); // phải đăng ký AppShell nếu dùng DI
            }
            else
            {
                var loginPage = services.GetRequiredService<LoginPage>();
                MainPage = new NavigationPage(loginPage);
            }
        }
    }

}



