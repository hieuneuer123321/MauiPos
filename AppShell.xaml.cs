using MauiAppUIDemo.Services;

namespace MauiAppUIDemo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));

            // Điều hướng dựa theo token ngay
            Task.Run(InitNavigationAsync);
        }

        private async Task InitNavigationAsync()
        {
            await Task.Delay(100); // ⏱ Chờ UI ổn định 1 chút

            var token = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                new ApiService().SetAccessToken(token);
                await MainThread.InvokeOnMainThreadAsync(() => GoToAsync("//MainPage"));
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() => GoToAsync("//LoginPage"));
            }
        }
    }


}
