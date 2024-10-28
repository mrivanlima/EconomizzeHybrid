using System.ComponentModel.DataAnnotations;


namespace EconomizzeHybrid.Model
{
    public class AddressModel
    {
        public int StreetId { get; set; }

        [Required(ErrorMessage = "Entre o complemento")]
        [Length(1, 255, ErrorMessage = "Maximo 255 caracteres.")]
        public string Complement { get; set; } = string.Empty;

        [Required(ErrorMessage = "Entre a rua")]
        [Length(1, 255, ErrorMessage = "Maximo 255 caracteres.")]
        public string StreetName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Entre o CEP")]
        [Length(8, 8, ErrorMessage = "Deve ser de 8 numeros")]
        public string ZipCode { get; set; } = string.Empty;
        public string NeighborhoodName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;

        public bool MainAddress { get; set; } = false;

        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
