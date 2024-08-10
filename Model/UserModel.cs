using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserMiddleName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
    }
}
