using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Criar
    {
        [SupplyParameterFromForm(FormName = "Register")]
        private RegisterModel register { get; set; } = new();

        private String message = String.Empty;
        private bool isVisible = false;
        private string? UserUniqueId { get; set; }
        private bool NewUser { get; set; } = true;
        private bool HideRegistration = false;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
            MessageHandler.Message = string.Empty;
        }

        private async Task RegisterUserAsync()
        {
            ArgumentNullException.ThrowIfNull(register);
            await UserLoginServices.CreateAsync(register);
            isVisible = true;
            if (!string.IsNullOrEmpty(MessageHandler.Message))
            {
                message = MessageHandler.Message;
                NewUser = false;
                return;
            }
            register = UserLoginServices.RegisteredUser;
            UserUniqueId = register.UserUniqueId.ToString();
            message = "Simular verificacao de email, clique aqui!";
            register = UserLoginServices.RegisteredUser;
            HideRegistration = true;
        }

        private async Task Verify()
        {
            if (NewUser)
            {
                ArgumentNullException.ThrowIfNull(register);
                await UserLoginServices.VerifyAsync(register);
                message = MessageHandler.Message;
            }
            NavigationManager.NavigateTo("login");
        }
    }
}