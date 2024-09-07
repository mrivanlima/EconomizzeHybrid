using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Classes
{
    internal class UserServices : IUserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MessageHandler _messageHandler;
        public UserModel? CurrentUserDetails { get; set; }
        public string Message { get; set; }
        private JsonSerializerOptions options { get; set; }

        public UserServices(IHttpClientFactory httpClientFactory, MessageHandler messageHandler)
        {
            _httpClientFactory = httpClientFactory;
            _messageHandler = messageHandler;
            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };
            Message = string.Empty;
        }

        public async Task ReadAsyncById(int id)
        {
            var message = string.Empty;
            var url = $"usuario/{id}";

            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CurrentUserDetails =  JsonSerializer.Deserialize<UserModel>(jsonResponse, options);
                }
                else
                {
                    CurrentUserDetails = null;
                    Message = jsonResponse.ToString();
                }
                //_navService.AddNavItem(new NavItem { Text = "Sair", Url = "login", Icon = "bi bi-box-arrow-right", IsVisible = true });

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public async Task CreateUserAsync(UserModel userModel, string userToken)
        {
            var url = $"usuario";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var response = await httpClient.PostAsJsonAsync(url, userModel);
                var jsonResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    CurrentUserDetails = JsonSerializer.Deserialize<UserModel>(jsonResponse, options);
                }
                else
                {
                    CurrentUserDetails = null;
                    _messageHandler.Message = jsonResponse.ToString();
                }
                //_navService.AddNavItem(new NavItem { Text = "Sair", Url = "login", Icon = "bi bi-box-arrow-right", IsVisible = true });

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
