using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Classes
{
    internal class UserServices : IUserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserModel CurrentUserDetails { get; set; }
        public string Message { get; set; }
        private JsonSerializerOptions options { get; set; }

        public UserServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };
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
    }
}
