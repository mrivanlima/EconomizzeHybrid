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
        UserLoginModel CurrentUser { get; set; }
        RegisterModel RegisteredUser { get; set; }
		ForgotPasswordModel PasswordDetails { get; set; }
		Task CreateAsync(RegisterModel register);
        Task ReadIdUuIdAsync(ForgotPasswordModel user);
        Task VerifyAsync(RegisterModel register);
        Task ReadAsync(UserLoginModel user);
        Task LogOut();
    }
}
