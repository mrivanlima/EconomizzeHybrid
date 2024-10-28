using EconomizzeHybrid.Model;
using Microsoft.AspNetCore.Components;

namespace EconomizzeHybrid.Components.Controls
{
    public partial class SearchZipCode
    {

        [Parameter]
        public EventCallback OnSuccess { get; set; }

        [Parameter]
        public EventCallback OnFailure { get; set; }

        private SearchZipCodeModel? zipCode { get; set; } = new();
        private String Message = String.Empty;
        private bool isVisible = false;

        private async Task BuscarCep()
        {
            ArgumentNullException.ThrowIfNull(zipCode);

            if (string.IsNullOrWhiteSpace(zipCode.ZipCode))
            {
                return;
            }

            await AddressServices.SearchZipCodeAsync(zipCode);
            isVisible = true;
            Message = AddressServices.Message;

            if (AddressServices.CurrentAddress is not null)
            {
                await OnSuccess.InvokeAsync();
            }
            else
            {
                await OnFailure.InvokeAsync();
            }
        }

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