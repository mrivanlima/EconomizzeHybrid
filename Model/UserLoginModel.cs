using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Model
{
    public class UserLoginModel
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Email necessario")]
        [EmailAddress(ErrorMessage = "Tem de ser um email")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha necessaria")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string Password { get; set; } = string.Empty;

        public string UserToken { get; set; } = string.Empty;
    }
}
