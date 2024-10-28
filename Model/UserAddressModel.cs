using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Model
{
    public class UserAddressModel
    {
        public int UserId { get; set; }

        public int StreetId { get; set; }

        public string Complement { get; set; } = string.Empty;

        public bool MainAddress { get; set; } = false;

        [Required]
        public int CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
