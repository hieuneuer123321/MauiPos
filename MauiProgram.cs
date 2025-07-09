using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MauiAppUIDemo.Services;
using MauiAppUIDemo.ViewModels;
namespace MauiAppUIDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("segmdl2.ttf", "Segmdl");   
                });
            builder.UseMauiApp<App>().UseMauiCommunityToolkit();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
