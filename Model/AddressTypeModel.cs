using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Model
{
    public class AddressTypeModel
    {
        public short AddressTypeId { get; set; }

        [Required]
        [StringLength(20)] // Assuming a max length of 100 for the name
        public string AddressTypeName { get; set; } = string.Empty;

        public string AddressTypeNameAscii { get; set; } = string.Empty;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
