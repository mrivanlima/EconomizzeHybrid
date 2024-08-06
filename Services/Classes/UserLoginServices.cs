using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EconomizzeHybrid.Services.Classes
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavService _navService;
        public UserLoginModel? CurrentUser { get; set; }
        public string Message { get; set; }
        private  JsonSerializerOptions Options {get; set;}

        public UserLoginServices(IHttpClientFactory httpClientFactory, NavService navService)
        {
            _httpClientFactory = httpClientFactory;
            _navService = navService;
            Options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true 
            };
            Message = String.Empty;
        }
        public async Task ReadAsync(UserLoginModel user)
        {
            var url = "register/Auth";
            
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.PostAsJsonAsync(url, user);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CurrentUser = JsonSerializer.Deserialize<UserLoginModel>(jsonResponse, Options);
					_navService.AddNavItem(new NavItemModel { Text = "Sair", Url = "login", Icon = "bi bi-box-arrow-right", IsVisible = true });
				}
                else
                {
                    Message = jsonResponse.ToString();
                }

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
