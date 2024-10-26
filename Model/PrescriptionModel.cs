using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeHybrid.Model
{
	public class PrescriptionModel
	{

		public long PrescriptionId { get; set; }

		public Guid PrescriptionUnique { get; set; }

		[StringLength(200)]
		public String PrescriptionUrl { get; set; } = String.Empty;

		public int FacilityId { get; set; }

		public int ProfessionalId { get; set; }

        [Required]
		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		public int ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }

		public String FileExtension { get; set; } = String.Empty;

        public byte[]? ImageData { get; set; } = null;

        public String Base64Image { get; set; } = String.Empty;
    }
}
