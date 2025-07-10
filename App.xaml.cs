using MauiAppUIDemo.Services;
using MauiAppUIDemo.ViewModels;

namespace MauiAppUIDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoadingPage(); // gán ngay
            InitRootPage(); // xử lý sau
        }

        private async void InitRootPage()
        {
            var token = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                AppServices.ApiService.SetAccessToken(token);
                MainPage = new AppShell(); // vào Main
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage(new LoginViewModel(AppServices.AuthService)));
            }
        }
    }
}



