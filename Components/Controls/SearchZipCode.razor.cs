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
            if (AddressServices.CurrentAddress is not null)
            {
                isVisible = false;
                await OnSuccess.InvokeAsync();
            }
            else
            {
                isVisible = true;
                Message = AddressServices.Message;
                await OnFailure.InvokeAsync();
            }
        }
    }
}