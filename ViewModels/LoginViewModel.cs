using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppUIDemo.Services;
using System.Threading.Tasks;

namespace MauiAppUIDemo.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
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
                // ✅ Lưu token an toàn
                await TokenStorage.SaveTokenAsync(
                        result.Data.AccessToken,
                        result.Data.RefreshToken,
                        result.Data.ExpiresIn
                    );

                // ✅ Gắn vào ApiService
                _authService.SetToken(result.Data.AccessToken); // hoặc gọi ApiService.Instance.SetAccessToken(...)
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
