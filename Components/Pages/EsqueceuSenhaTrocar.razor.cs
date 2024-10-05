using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class EsqueceuSenhaTrocar
    {
        #region VARIABLES
        //FORM parameters
        [SupplyParameterFromForm(FormName = "PasswordChange")]
        private ForgotPasswordModel ForgotPassword { get; set; } = new();

        //URL parameters
        [Parameter]
        public string? UserId { get; set; }

        [Parameter]
        public string? UserUniqueId { get; set; }

        //Set necessary variables
        private string username = String.Empty;
        private String message = String.Empty;
        private bool isVisible = false;
        #endregion

        #region INITIALIZE
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);

            //initialize message as empty
            MessageHandler.Message = string.Empty;

            //initialize username entered in previous form
            username = UsernameService.SharedUsername.username;
            ForgotPassword.Username = username;

            ForgotPassword.UserId = int.Parse(UserId);
        }
        #endregion

        #region SUBMIT
        private async Task HandleValidSubmit()
        {
            //update password
            await ChangePasswordAsync();
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
        #endregion

        #region CHANGE PASSWORD
        private async Task ChangePasswordAsync()
        {
            //change password and show message
            await PasswordServices.UpdateForgotPasswordAsync(ForgotPassword, ForgotPassword.Username);
            await StatusHandling();
        }
        #endregion

        #region STATUS
        private async Task StatusHandling()
        {
            //make message box visible and set current message
            isVisible = true;
            message = MessageHandler.Message;

            //refresh UI
            StateHasChanged();

            if (!PasswordServices.isError)
            {
                //Display redirection message
                await Task.Delay(5000);

                message = "Redirecting...";
                StateHasChanged();

                await Task.Delay(3000);

                //redirect to main page for login
                NavigationManager.NavigateTo("/login");
            }
        }
        #endregion
    }
}