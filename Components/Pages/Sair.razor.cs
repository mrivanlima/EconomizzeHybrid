using EconomizzeHybrid.Services.Components;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Sair
    {
        private bool hideLoad = false;

        protected override async Task OnInitializedAsync()
        {

            await Task.Delay(100);
            UserLoginServices.LogOut();
            await CacheServices.LogOut();
            await NavService.RemoveNavItem("Sair");
            hideLoad = false;
            NavigationManager.NavigateTo("login");

        }

    }
}