using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EconomizzeHybrid.Services.Classes
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavService _navService;
        public UserLoginModel CurrentUser { get; set; }
        public string Message { get; set; }
        private  JsonSerializerOptions options {get; set;}

        public UserLoginServices(IHttpClientFactory httpClientFactory, NavService navService)
        {
            _httpClientFactory = httpClientFactory;
            _navService = navService;
            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true 
            }; 
        }
        public async Task ReadAsync(UserLoginModel user)
        {
            var message = string.Empty;
            var url = "register/Auth";
            
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.PostAsJsonAsync(url, user);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CurrentUser = JsonSerializer.Deserialize<UserLoginModel>(jsonResponse, options);   
                }
                else
                {
                    CurrentUser = null;
                    Message = jsonResponse.ToString();
                }
                _navService.AddNavItem(new NavItem { Text = "Sair", Url = "login", Icon = "bi bi-box-arrow-right", IsVisible = true });

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }


        }

        public void LogOut()
        {
            CurrentUser = null;
            _navService.RemoveNavItem("login");
        }
    }
}
