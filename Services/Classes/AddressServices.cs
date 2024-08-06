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
    public class AddressServices : IAddressServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AddressModel? CurrentAddress { get; set; }
        public string Message { get; set; }

        public AddressServices(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions) 
        {
            _httpClientFactory = httpClientFactory;
            _jsonSerializerOptions = jsonSerializerOptions;
            Message = String.Empty;
        }

        public async Task SearchZipCodeAsync(string zipCode)
        {
            var url = $"endereco/{zipCode}";

            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CurrentAddress = JsonSerializer.Deserialize<AddressModel>(jsonResponse, _jsonSerializerOptions);
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
    }
}
