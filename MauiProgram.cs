
using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            var JsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
            };


            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddHttpClient("economizze", config =>
            {
                config.BaseAddress = new Uri("http://economizze.app/api/");
            });
            //builder.Services.AddHttpClient("economizze", config =>
            //{
            //    config.BaseAddress = new Uri("https://localhost:7255/api/");
            //});
            //http://localhost:5240
            //http://economizze.app/api/



            builder.Services.AddSingleton<IUserLoginServices,  UserLoginServices>();
            builder.Services.AddSingleton<UserLoginSqliteServices>();
            builder.Services.AddSingleton<IUserServices, UserServices>();
            builder.Services.AddSingleton(JsonSerializerOptions);

            builder.Services.AddScoped<IAddressServices, AddressServices>();
            builder.Services.AddScoped<IAddressTypeServices, AddressTypeServices>();

            builder.Services.AddSingleton<NavService>();

            builder.Services.AddSingleton<IDatabaseConnectionFactory, SqlLiteConnection>();
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
