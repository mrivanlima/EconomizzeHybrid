using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Classes;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Controls
{
    public partial class QuoteCreate
    {
        private QuoteModel Quote { get; set; } = new();
        private AddressModel address { get; set; } = new();
        private UserLoginModel? currentUser { get; set; }


        private string message = string.Empty;

        [Parameter]
        public bool canCreateQuote { get; set; } = false;
        private bool showEndereco = false;
        private bool isVisible = false;

        #region INITIALIZE
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
            currentUser = UserLoginServices.CurrentUser;
            Quote.UserId = UserLoginServices.CurrentUser.UserId;
            MessageHandler.Message = string.Empty;
            await FindMainAddress();
        }
        #endregion

        #region FIND MAIN ADDRESS
        private async Task FindMainAddress()
        {
            //search for any information to be pulled from cache
            await CacheServices.ReadAddress(currentUser!.UserId);

            //if any information is pulled, add to address fields on load
            if (CacheServices.UserAddress is not null)
            {
                address = CacheServices.UserAddress;
                AddressServices.CurrentAddress = address;
            }
        }
        #endregion

        #region HANDLE VALID SUBMIT
        private async Task HandleValidSubmit()
        {
            await Task.Delay(0);
        }
        #endregion

        #region IF USER WANTS TO SEND TO MAIN ADDRESS
        private async Task UseMainAddress()
        {
            await Task.Delay(0);
            canCreateQuote = true;
        }
        #endregion

        #region ADD NEW ADDRESS
        private async Task ShowEndereco()
        {
            await Task.Delay(0);
            showEndereco = true;
			StateHasChanged();
        }
        #endregion

        #region CREATE QUOTE
        private async Task CreateQuote()
        {
			address = AddressServices.CurrentAddress;
			await QuoteServices.FindNeighborhoodId(address.StreetId);
            
            if (!QuoteServices.isError)
            {
                Quote.NeighborhoodId = QuoteServices.Quote.NeighborhoodId;
				await QuoteServices.CreateQuoteAsync(Quote);
			}
            //message = MessageHandler.Message;
            //isVisible = true;

            //StateHasChanged();
            //await OnParametersSetAsync();
            NavigationManager.NavigateTo($"/prescription-management");
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
    }
}