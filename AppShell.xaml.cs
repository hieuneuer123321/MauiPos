using MauiAppUIDemo.Services;

namespace MauiAppUIDemo
{
    public partial class AppShell : Shell
    {
        private readonly IApiService _apiService;

        public AppShell(IApiService apiService)
        {
            InitializeComponent();

            _apiService = apiService;

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        }

        public async Task InitNavigationAsync()
        {
            await Task.Delay(100);

            var token = await TokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                _apiService.SetAccessToken(token);
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }

}



