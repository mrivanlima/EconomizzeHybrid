using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface ICacheServices
    {
        UserLoginModel CurrentUser { get; set; }
        Task CreateUserSession(UserLoginModel userLoginModel);
        Task LogOut();
        Task SearchActiveSession();
        Task AddUserDetails(UserModel model);
        Task AddUserAddress(AddressModel model);
    }
}
