using MauiAppUIDemo.ViewModels;

namespace MauiAppUIDemo;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}