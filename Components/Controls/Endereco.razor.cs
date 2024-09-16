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

        protected override async Task OnInitializedAsync()
        {
            //Set currentUser
            currentUser = UserLoginServices.CurrentUser;

            //Send back to login page if no user is found
            if (currentUser is null)
            {
                NavigationManager.NavigateTo("login");
            }

            //Read all address types for the drop-down menu
            await AddressTypeServices.AddressTypeReadAll();
            addressTypesModel = AddressTypeServices.AddressTypes;

            //Search for Address in Cache, if not there then in API
            await SearchAddressInCache();
            await SearchAddressFromAPI();
        }

        #region SUBMIT
        private void HandleSuccess()
        {

            //populate fields when zipCode is found
            address = AddressServices.CurrentAddress;
            address.UserId = currentUser.UserId;
            address.CreatedBy = currentUser.UserId;
            address.ModifiedBy = currentUser.UserId;
        }

        private void HandleFailure()
        {
            //null address if zipcode cannot be found
            address = new();
        }

        private async Task HandleValidSubmit()
        {
            //if valid user, add address to cache
            if (address is not null && currentUser is not null)
            {
                await AddUserAddress();
                await AddUserAddressToCache();

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
            //alert box color is red if error while adding address, green if no errors
            var color = AddressServices.isError ? "background-color:#FF928D;" : "background-color:#8DFFB6;";

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
        private async Task AddUserAddress()
        {
            //Add user address to the API
            await AddressServices.CreateUserAddressAsync(address);
        }

        private async Task AddUserAddressToCache()
        {
            if (AddressServices.CurrentAddress is not null)
            {
                //Add user address to the cache
                await CacheServices.AddUserAddress(address);
            }
        }
        #endregion

        #region SEARCH FUNCTIONS
        private async Task SearchAddressInCache()
        {
            //Read any address in the cache
            await CacheServices.ReadAddress(currentUser.UserId);

            //If address has been acquired, then add to address
            if (CacheServices.UserAddress is not null)
            {
                address = CacheServices.UserAddress;
                AddressServices.CurrentAddress = address;
            }
        }

        private async Task SearchAddressFromAPI()
        {
            //Continue if no address has been found in cache
            if (AddressServices.CurrentAddress is null)
            {
                //Read from cache
                await AddressServices.ReadAsyncById(currentUser.UserId);

                //If CurrentAddress has information, add to address then add to cache
                if (AddressServices.CurrentAddress is not null)
                {
                    address = AddressServices.CurrentAddress;
                    await AddUserAddressToCache();
                }
            }
        }
        #endregion
    }
}