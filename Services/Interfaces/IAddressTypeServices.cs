using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IAddressTypeServices
    {
        Task AddressTypeReadAll();
        IEnumerable<AddressTypeModel>? AddressTypes { get; set; }
    }
}
