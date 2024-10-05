// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Pages
{
    public partial class Login
    {
        [SupplyParameterFromForm(FormName = "Login")]
        private UserLoginModel userLogin { get; set; } = new();
        private String message = String.Empty;
        private bool isVisible = false;
        private bool hideLoad = false;
        private bool hideContent = true;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
            hideLoad = true;
            hideContent = false;
        }

        private async Task LoginUserAsync()
        {
            await Task.Delay(0);
            await FetchUser();
            if (UserLoginServices.CurrentUser is not null)
            {
                userLogin = UserLoginServices.CurrentUser;
                await CreateSession();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                message = MessageHandler.Message;
                isVisible = true;
            }
        }

        private async Task FetchUser()
        {
            await UserLoginServices.ReadAsync(userLogin);
        }

        private async Task CreateSession()
        {
            await Task.Delay(0);
            if (UserLoginServices.CurrentUser is not null)
            {
                await CacheServices.CreateUserSession(userLogin);
            }
        }
    }
}
