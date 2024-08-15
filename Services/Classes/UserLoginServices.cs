using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Components;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EconomizzeHybrid.Services.Classes
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MessageHandler _messageHandler;
        public UserLoginModel? CurrentUser { get; set; }
        public RegisterModel? RegisteredUser { get; set; }
        private  JsonSerializerOptions Options {get; set;}

        private readonly NavService _navService;

        public UserLoginServices(IHttpClientFactory httpClientFactory, 
                                 NavService navService,
                                 MessageHandler messageHandler)
        {
            _httpClientFactory = httpClientFactory;
            _messageHandler = messageHandler;

            //_sqliteDb = sqliteDb;
            Options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true 
            };
            _navService = navService;
        }
        public async Task ReadAsync(UserLoginModel user)
        {
            var url = "conta/autenticar";
            
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.PostAsJsonAsync(url, user);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CurrentUser = JsonSerializer.Deserialize<UserLoginModel>(jsonResponse, Options);
				}
                else
                {
                    _messageHandler.Message = jsonResponse.ToString();
                }

            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
        }

        public async Task CreateAsync(RegisterModel register)
        {
            var url = "conta/criar";

            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.PostAsJsonAsync(url, register);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {


                    RegisteredUser = JsonSerializer.Deserialize<RegisterModel>(jsonResponse, Options);
                    CurrentUser = new();
                    CurrentUser.UserId = RegisteredUser.UserId;
                    CurrentUser.Username = RegisteredUser.Username;
                    CurrentUser.Password = RegisteredUser.Password;
                    //_navService.AddNavItem(new NavItemModel { Text = "Sair", Url = "login", Icon = "bi bi-box-arrow-right", IsVisible = true });
                }
                else
                {
                    _messageHandler.Message = jsonResponse.ToString();
                }

            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
        }

        public async Task VerifyAsync(RegisterModel register)
        {
            var url = $"conta/verificar/{register.UserId}?userUniqueId={register.UserUniqueId}";

            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    _messageHandler.Message = "Verificacao concluida! Faca o Login";
                }
                else
                {
                    _messageHandler.Message = jsonResponse.ToString();
                }

            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
        }

        public async Task LogOut()
        {

            CurrentUser = null;
            await _navService.RemoveNavItem("login");
            _messageHandler.Message = string.Empty;
        }
    }
}
