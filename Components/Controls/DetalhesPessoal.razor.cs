using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Controls
{
    public partial class DetalhesPessoal
    {
        #region VARIABLES
        private UserModel userModel = new();
        private UserLoginModel userLoginModel = new();
        private bool hideLoad = false;
        private bool hideContent = true;
        private String message = String.Empty;
        private bool isVisible = false;
        private int UserId { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            //set userModel
            userModel.UserId = UserLoginServices.CurrentUser.UserId;
            userModel.UserEmail = UserLoginServices.CurrentUser.Username;

            //search in cache, if not there then search API
            await SearchUserInCache();
            await SearchUserFromAPI();

            hideLoad = true;
            hideContent = false;
        }

        #region SUBMIT
        private async Task HandleValidSubmit()
        {
            //if valid user, add details to cache
            if (userModel is not null && userLoginModel is not null)
            {
                await AddUserDetails();
                await AddUserDetailsToCache();

                //display success or error message
                message = MessageHandler.Message;
                isVisible = true;
            }

            //set time that alert stays on screen
            OnParametersSetAsync();
        }
        #endregion

        #region STYLE FUNCTIONS
        private string GetStyle()
        {
            //alert box color is red if error while adding details, green if no errors
            var color = UserServices.isError ? "background-color:#FF928D;" : "background-color:#8DFFB6;";

            //display block when submit button is pressed
            var display = isVisible ? "display:block;" : "display:none;";

            //return string in style for the CSS to work properly
            return color + " " + display;
        }

        protected override async Task OnParametersSetAsync()
        {
            //when alert box is visible
            if (isVisible)
            {
                //wait 7.5 seconds and then hide the box
                await Task.Delay(7500);
                isVisible = false;
                StateHasChanged();
            }
        }
        #endregion

        #region ADD FUNCTIONS
        private async Task AddUserDetails()
        {
            //Add user details to the API
            await UserServices.CreateUserAsync(userModel, UserLoginServices.CurrentUser.UserToken);
        }

        private async Task AddUserDetailsToCache()
        {
            if (UserServices.CurrentUserDetails is not null)
            {
                //Add user details to the cache
                await CacheServices.AddUserDetails(userModel);
            }
        }
        #endregion

        #region SEARCH FUNCTIONS
        private async Task SearchUserInCache()
        {
            //Read any details in the cache
            await CacheServices.ReadUserDetails(userModel.UserId);

            //If details have been acquired, then add to userModel
            if (CacheServices.UserDetails is not null)
            {
                userModel = CacheServices.UserDetails;
                userModel.UserEmail = UserLoginServices.CurrentUser.Username;
                UserServices.CurrentUserDetails = userModel;
            }
        }

        private async Task SearchUserFromAPI()
        {
            //Continue if no details have been found in cache
            if (UserServices.CurrentUserDetails is null)
            {
                //Read from cache
                await UserServices.ReadAsyncById(userModel.UserId);

                //If CurrentUserDetails has information, add to userModel then add to cache
                if (UserServices.CurrentUserDetails is not null)
                {
                    userModel = UserServices.CurrentUserDetails;
                    userModel.UserEmail = UserLoginServices.CurrentUser.Username;
                    await AddUserDetailsToCache();
                }
            }
        }
        #endregion
    }
}