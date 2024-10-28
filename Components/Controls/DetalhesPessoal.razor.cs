using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;

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

        #region INITIALIZE
        protected override async Task OnInitializedAsync()
        {
            //set userModel
            userModel.UserId = UserLoginServices.CurrentUser.UserId;
            userModel.UserEmail = UserLoginServices.CurrentUser.Username;

            //search through cache then API for any details already registered
            await SearchUserInCache();
            await SearchUserFromAPI();

            hideLoad = true;
            hideContent = false;
        }
        #endregion

        #region SUBMIT
        private async Task HandleValidSubmit()
        {
            //if valid user with valid details filled
            if (userModel is not null && userLoginModel is not null)
            {
                //add details to API and then cache
                await AddUserDetails();
                await AddUserDetailsToCache();

                //display success / error message
                message = MessageHandler.Message;
                isVisible = true;
            }

            //function called to keep alert up for set amount of time
            await OnParametersSetAsync();
        }
        #endregion

        #region STYLE
        private string GetStyle()
        {
            //if a submit error is detected, red background. If not then green
            var color = UserServices.isError ? "background-color:#FF928D;" : "background-color:#8DFFB6;";

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

        #region ADD
        private async Task AddUserDetails()
        {
            //add details to the API
            await Task.Delay(0);
            await UserServices.CreateUserAsync(userModel, UserLoginServices.CurrentUser.UserToken);
        }

        private async Task AddUserDetailsToCache()
        {
            //if details have been properly added to API, add to cache
            if (UserServices.CurrentUserDetails is not null)
            {
                await CacheServices.AddUserDetails(userModel);
            }
        }
        #endregion

        #region SEARCH
        private async Task SearchUserInCache()
        {
            //search for any information to be pulled from cache
            await Task.Delay(0);
            await CacheServices.ReadUserDetails(userModel.UserId);

            //if any information is pulled, add details to fields on load
            if (CacheServices.UserDetails is not null)
            {
                userModel = CacheServices.UserDetails;
                userModel.UserEmail = UserLoginServices.CurrentUser.Username;
                UserServices.CurrentUserDetails = userModel;
            }
        }

        private async Task SearchUserFromAPI()
        {
            //if no details are found in cache
            if (UserServices.CurrentUserDetails is null)
            {
                //search though API for details
                await UserServices.ReadAsyncById(userModel.UserId);

                //if details are found in API
                if (UserServices.CurrentUserDetails is not null)
                {
                    //set userModel
                    userModel = UserServices.CurrentUserDetails;
                    userModel.UserEmail = UserLoginServices.CurrentUser.Username;

                    //add details to cache
                    await AddUserDetailsToCache();
                }
            }
        }
        #endregion
    }
}