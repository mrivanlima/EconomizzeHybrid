using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EconomizzeHybrid
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
                

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddHttpClient("economizze", config =>
            {
                config.BaseAddress = new Uri("https://localhost:7255/api/");
            });
            //builder.Services.AddHttpClient("economizze", config =>
            //{
            //    config.BaseAddress = new Uri("https://localhost:7255/api/");
            //});

            

            builder.Services.AddSingleton<IUserLoginServices,  UserLoginServices>();
            builder.Services.AddSingleton<IUserServices, UserServices>();
            builder.Services.AddSingleton<NavService>();
            //builder.Services.AddSingleton(sp =>
            //new HttpClient
            //{
            //    BaseAddress = new Uri("https://localhost:7255/api/")
            //});

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            //builder.Services.AddSingleton<UserLogin>();
            return builder.Build();
        }
    }
}
