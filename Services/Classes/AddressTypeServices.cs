using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Classes
{
    internal class AddressTypeServices : IAddressTypeServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public IEnumerable<AddressTypeModel>? AddressTypes { get; set; }

        public string Message { get; set; }

        public AddressTypeServices(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClientFactory = httpClientFactory;
            _jsonSerializerOptions = jsonSerializerOptions;
            Message = String.Empty;
            //AddressTypes = new List<AddressTypeModel>();
        }

        
        public async Task AddressTypeReadAll()
        {
            var url = $"tipodeendereco";
            try
            {
                var httpClient = _httpClientFactory.CreateClient("economizze");
                var response = await httpClient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    AddressTypes = JsonSerializer.Deserialize<IEnumerable<AddressTypeModel>>(jsonResponse, _jsonSerializerOptions);
                }
                else
                {
                    AddressTypes = null;
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
