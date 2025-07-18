using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppUIDemo.Services;
using System.Threading.Tasks;

namespace MauiAppUIDemo.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IApiService _apiService;

    public LoginViewModel(IAuthService authService, IApiService apiService)
    {
        _authService = authService;
        _apiService = apiService;
    }

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isError;

    [ObservableProperty]
    private string errorMessage;

    [RelayCommand]
    private async Task LoginAsync()
    {
        IsError = false;
        ErrorMessage = "";
        IsBusy = true;

        try
        {
            var result = await _authService.LoginAsync(Email, Password);

            if (result.Succeeded)
            {
                // Lưu token
                await TokenStorage.SaveTokenAsync(
                    result.Data.AccessToken,
                    result.Data.RefreshToken,
                    result.Data.ExpiresIn
                );

                // ✅ Lưu user info
                await UserSessionService.SaveUserAsync(result.Data.User);

                // Gán token vào ApiService nếu cần
                _authService.SetToken(result.Data.AccessToken);
                await _apiService.InitializeTokenAsync();            // ✅ Đồng bộ lại _accessToken từ TokenStorage

                // ✅ Đặt AppShell làm MainPage TRƯỚC
                Application.Current.MainPage = new AppShell();

                // ✅ Bây giờ Shell.Current mới không null
                await Shell.Current.GoToAsync("//MainPage");

            }
            else
            {
                IsError = true;
                ErrorMessage = result.Message ?? "Đăng nhập thất bại.";
            }
        }
        catch (Exception ex)
        {
            IsError = true;
            ErrorMessage = "Lỗi hệ thống: " + ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
