using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Controls
{
    public partial class Endereco
    {
        #region VARIABLES
        private AddressModel? address = new();
        private UserLoginModel? currentUser { get; set; }
        private bool isSubmitted = false;
        private IEnumerable<AddressTypeModel>? addressTypesModel;
        private String message = String.Empty;
        private bool isVisible = false;
        #endregion

        #region INITIALIZE
        protected override async Task OnInitializedAsync()
        {
            //set currentUser
            currentUser = UserLoginServices.CurrentUser;

            //if no user found, navigate to login
            if (currentUser is null)
            {
                NavigationManager.NavigateTo("login");
            }

            //read all address types to supply the drop down menu
            await AddressTypeServices.AddressTypeReadAll();
            addressTypesModel = AddressTypeServices.AddressTypes;

            //search through cache and then API for any address already registered
            await SearchAddressInCache();
            await SearchAddressFromAPI();
        }
        #endregion

        #region SUBMIT
        private void HandleSuccess()
        {
            //if zip code is found, add all appropriate details to fields
            address = AddressServices.CurrentAddress;
            address.UserId = currentUser.UserId;
            address.CreatedBy = currentUser.UserId;
            address.ModifiedBy = currentUser.UserId;
        }

        private void HandleFailure()
        {
            //null address if zip code cannot be found
            address = new();
        }

        private async Task HandleValidSubmit()
        {
            //if valid user with valid address filled
            if (address is not null && currentUser is not null)
            {
                //add address to API and then cache
                await AddUserAddress();
                await AddUserAddressToCache();

                //display success / error message
                message = MessageHandler.Message;
                isVisible = true;
            }

            //function called to keep alert up for set amount of time
            OnParametersSetAsync();
        }
        #endregion

        #region STYLE
        private string GetStyle()
        {
            //if a submit error is detected, red background. If not then green
            var color = AddressServices.isError ? "background-color:#FF928D;" : "background-color:#8DFFB6;";

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
        private async Task AddUserAddress()
        {
            //add address to the API
            await Task.Delay(0);
            await AddressServices.CreateUserAddressAsync(address);
        }

        private async Task AddUserAddressToCache()
        {
            //if address have been properly added to API, add to cache
            if (AddressServices.CurrentAddress is not null)
            {
                await CacheServices.AddUserAddress(address);
            }
        }
        #endregion

        #region SEARCH
        private async Task SearchAddressInCache()
        {
            //search for any information to be pulled from cache
            await Task.Delay(0);
            await CacheServices.ReadAddress(currentUser.UserId);

            //if any information is pulled, add to address fields on load
            if (CacheServices.UserAddress is not null)
            {
                address = CacheServices.UserAddress;
                AddressServices.CurrentAddress = address;
            }
        }

        private async Task SearchAddressFromAPI()
        {
            //if no address is found in cache
            if (AddressServices.CurrentAddress is null)
            {
                //search though API for address
                await AddressServices.ReadAsyncById(currentUser.UserId);

                //if address is found in API
                if (AddressServices.CurrentAddress is not null)
                {
                    //set address
                    address = AddressServices.CurrentAddress;

                    //add user address to cache
                    await AddUserAddressToCache();
                }
            }
        }
        #endregion
    }
}