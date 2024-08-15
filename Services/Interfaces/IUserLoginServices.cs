using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IUserLoginServices
    {
        Task ReadAsync(UserLoginModel user);
        Task VerifyAsync(RegisterModel register);
        UserLoginModel CurrentUser { get; set; }
        RegisterModel RegisteredUser { get; set; }
        Task CreateAsync(RegisterModel register);
        Task LogOut();
    }
}
