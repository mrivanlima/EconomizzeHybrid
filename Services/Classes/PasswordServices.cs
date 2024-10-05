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
    internal class PasswordServices : IPasswordServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MessageHandler _messageHandler;
        public LoggedInPasswordModel? SetCurrentPassword { get; set; }
        private JsonSerializerOptions options { get; set; }
        public bool isError { get; set; }

        #region CONSTRUCTOR
        public PasswordServices(IHttpClientFactory httpClientFactory, MessageHandler messageHandler)
        {
            _httpClientFactory = httpClientFactory;
            _messageHandler = messageHandler;
            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };
        }
        #endregion

        #region Update password for logged in user
        public async Task UpdatePasswordAsync(LoggedInPasswordModel passwordModel, string userToken)
        {
            var url = $"conta/trocarSenha";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var response = await httpClient.PutAsJsonAsync(url, passwordModel);
                var jsonResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    isError = false;
                    SetCurrentPassword = JsonSerializer.Deserialize<LoggedInPasswordModel>(jsonResponse, options);
                    _messageHandler.Message = "Sucesso!";
                }
                else
                {
                    isError = true;
                    _messageHandler.Message = jsonResponse.ToString();
                }
            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
        }
        #endregion

        #region Update password for user if forgot password
        public async Task UpdateForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel, string username)
        {
            var url = $"conta/trocarSenha/esqueceu";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.PutAsJsonAsync(url, forgotPasswordModel);
                var jsonResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    isError = false;
                    _messageHandler.Message = "Sucesso!";
                }
                else
                {
                    isError = true;
                    _messageHandler.Message = jsonResponse.ToString();
                }
            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
        }
        #endregion
    }
}
