using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IAddressServices
    {
        Task SearchZipCodeAsync(SearchZipCodeModel CurrentZipCode);
        AddressModel CurrentAddress { get; set; }
        SearchZipCodeModel CurrentZipCode { get; set; }
        string Message { get; set; }
    }
}
