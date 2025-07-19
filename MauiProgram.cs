using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MauiAppUIDemo.Services;
using MauiAppUIDemo.ViewModels;
using MauiAppUIDemo.Views;
using MauiAppUIDemo.Helopper;
namespace MauiAppUIDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("segmdl2.ttf", "Segmdl");
                });

            // Đăng ký services
            builder.Services.AddTransient<AuthHandler>();
            builder.Services.AddHttpClient<IApiService, ApiService>()
                            .AddHttpMessageHandler<AuthHandler>();

            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddTransient<OrderPageViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();

            var app = builder.Build();
            ServiceHelper.Services = app.Services;
            return app;
        }
    }

}
