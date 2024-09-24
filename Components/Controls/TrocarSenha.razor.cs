using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Controls
{
    public partial class TrocarSenha
    {
        #region VARIABLES
        private LoggedInPasswordModel? password = new();
        private UserLoginModel? currentUser { get; set; }
        private bool isSubmitted = false;
        private String message = String.Empty;
        private bool isVisible = false;
        #endregion

        #region INITIALIZE
        protected override async Task OnInitializedAsync()
        {
            
           currentUser = UserLoginServices.CurrentUser;

            //set passwordModel
            await Task.Delay(0);
            password.UserId = UserLoginServices.CurrentUser.UserId;
            password.CurrentPassword = UserLoginServices.CurrentUser.Password;
        }
        #endregion

        #region SUBMIT
        private async Task HandleValidSubmit()
        {
            //if valid user with valid password filled
            if (password is not null && currentUser is not null)
            {
                //Update password in db
                await UpdatePassword();

                //display success / error message
                message = MessageHandler.Message;
                isVisible = true;
            }

            //function called to keep alert up for set amount of time
            OnParametersSetAsync();
        }
        #endregion

        #region STYLE
        private string GetStyle()
        {
            //if a submit error is detected, red background. If not then green
            var color = PasswordServices.isError ? "background-color:#FF928D;" : "background-color:#8DFFB6;";

            //display block on valid submit
            var display = isVisible ? "display:block;" : "display:none;";

            //return css string
            return color + " " + display;
        }

        protected override async Task OnParametersSetAsync()
        {
            //if block is visible, wait 7.5 seconds before hiding again
            if (isVisible)
            {
                await Task.Delay(7500);
                isVisible = false;
                StateHasChanged();
            }
        }
        #endregion

        #region UPDATE
        private async Task UpdatePassword()
        {
            //add updated password to the db
            await Task.Delay(0);
            await PasswordServices.UpdatePasswordAsync(password, currentUser.UserToken);
        }
        #endregion
    }
}