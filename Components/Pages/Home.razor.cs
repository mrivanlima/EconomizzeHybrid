using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Components;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Home
    {
        private UserLoginModel? currentUser = new();
        private UserModel? currentUserDetails = new();

        protected override async Task OnInitializedAsync()
        {
            await SearchActiveUser();
            if (UserLoginServices.CurrentUser is null)
            {
                NavigationManager.NavigateTo("login");
            }
            else
            {
                await CreateNavigationItem();
                NavigationManager.NavigateTo("perfil");
            }
        }

        private async Task SearchActiveUser()
        {
            if (UserLoginServices.CurrentUser is null)
            {
                await CacheServices.SearchActiveSession();
                UserLoginServices.CurrentUser = CacheServices.CurrentUser;
            }
        }

        private async Task CreateNavigationItem()
        {
            if (UserLoginServices.CurrentUser is not null)
            {
                await NavService.AddNavItem
                (
                    new NavItemModel
                    {
                        Text = "Sair",
                        Url = "Sair",
                        Icon = "bi bi-box-arrow-right",
                        IsVisible = true
                    }
                );
                NavigationManager.NavigateTo("perfil");
            }
        }

        private async Task LogOut()
        {
            await NavService.RemoveNavItem("sair");
            UserLoginServices.LogOut();
            await CacheServices.LogOut();

        }
    }
}