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
        UserLoginModel CurrentUser { get; set; }
        string Message { get; set; }
        void LogOut();
    }
}
