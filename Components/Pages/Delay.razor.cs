using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Delay
    {
        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo("Login");
        }
    }
}