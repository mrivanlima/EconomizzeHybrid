using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IPasswordServices
    {
        Task UpdatePasswordAsync(LoggedInPasswordModel passwordModel, string userToken);
        LoggedInPasswordModel SetCurrentPassword { get; set; }
        bool isError { get; set; }
    }
}
