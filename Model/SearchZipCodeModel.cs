using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Model
{
    public class SearchZipCodeModel
    {
        [Required(ErrorMessage = "Entre o CEP")]
        [Length(8, 8, ErrorMessage = "Deve ser de 8 numeros")]
        public string ZipCode {  get; set; } = string.Empty;
    }
}
