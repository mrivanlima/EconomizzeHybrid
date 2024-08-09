using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Classes
{
    public class UserAddressServices : IUserAddressServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AddressModel? CurrentAddress { get; set; }
        public SearchZipCodeModel CurrentZipCode { get; set; }
        public string Message { get; set; }

        public UserAddressServices(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClientFactory = httpClientFactory;
            _jsonSerializerOptions = jsonSerializerOptions;
            Message = String.Empty;
        }
    }
}
