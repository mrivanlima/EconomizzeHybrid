using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class EsqueceuSenha
    {
        [SupplyParameterFromForm(FormName = "Email")]
        public UsernameModel Username { get; set; } = new();

        private ForgotPasswordModel forgotPassword { get; set; } = new();
        private String message = String.Empty;
        private bool isVisible = false;

        //parameters
        public Guid? UserUniqueId { get; set; }
		public int? UserId { get; set; }

        //hiding values
        private bool NewPassword { get; set; } = true;
        private bool HideEmail = false;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
            MessageHandler.Message = string.Empty;
        }

        private async Task SendEmailAsync()
        {
            isVisible = true;
            UsernameService.SharedUsername.username = Username.username;
            forgotPassword.Username = Username.username;
            if (!string.IsNullOrEmpty(MessageHandler.Message))
            {
                message = MessageHandler.Message;
                NewPassword = false;
                return;
            }
            await UserLoginServices.ReadIdUuIdAsync(forgotPassword);
			UserId = UserLoginServices.PasswordDetails.UserId;
			UserUniqueId = UserLoginServices.PasswordDetails.UserUniqueId;
			message = "Simular troca de senha, clique aqui!";
            //HideEmail = true;
        }

        private async Task Navigate()
        {
            //isVisible = false;
            await Task.Delay(0);
			if (UserId is not null && UserUniqueId != Guid.Empty)
			{
				NavigationManager.NavigateTo($"/TrocarSenha/{UserId}/{UserUniqueId}");
			}
			else
			{
				MessageHandler.Message = "Invalid User ID or Unique ID.";
			}
		}
    }
}