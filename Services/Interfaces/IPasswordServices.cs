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
        //change password for logged in user
        Task UpdatePasswordAsync(LoggedInPasswordModel passwordModel, string userToken);

        //change password for user if forgot password
        Task UpdateForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel, string username);

        //set new password after updating
        LoggedInPasswordModel SetCurrentPassword { get; set; }

        //check for errors
        bool isError { get; set; }
    }
}
