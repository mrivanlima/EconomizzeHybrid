using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Delay
    {

        protected override async Task OnInitializedAsync()
        {
            NavigationManager.NavigateTo("Login");
        }
    }
}