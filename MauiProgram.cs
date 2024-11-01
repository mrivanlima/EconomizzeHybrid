﻿using Blazored.Modal;
using EconomizzeHybrid.Services.Classes;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            var JsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
            };


            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal();

            builder.Services.AddHttpClient("economizze", config =>
            {
                config.BaseAddress = new Uri("https://localhost:7255/api/");
            });
            //builder.Services.AddHttpClient("economizze", config =>
            //{
            //    config.BaseAddress = new Uri("https://localhost:7255/api/");
            //});
            //http://localhost:5240
            //http://economizze.app/api/



            builder.Services.AddSingleton<IUserLoginServices,  UserLoginServices>();
            builder.Services.AddSingleton<CacheServices>();
			builder.Services.AddSingleton<IUserServices, UserServices>();
			builder.Services.AddSingleton<IQuoteServices, QuoteServices>();
            builder.Services.AddSingleton <IPrescriptionServices, PrescriptionServices>();
			builder.Services.AddSingleton<IPasswordServices, PasswordServices>();
            builder.Services.AddSingleton(JsonSerializerOptions);

            builder.Services.AddScoped<UsernameService>();
            builder.Services.AddScoped<IAddressServices, AddressServices>();

            builder.Services.AddSingleton<NavService>();

            builder.Services.AddSingleton<ICacheFactory, CacheConnection>();
            builder.Services.AddSingleton<ICacheServices, CacheServices>();
            
            builder.Services.AddSingleton<MessageHandler>();
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
