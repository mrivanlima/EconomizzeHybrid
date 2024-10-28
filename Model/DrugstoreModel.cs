using System.ComponentModel.DataAnnotations;

namespace EconomizzeHybrid.Model
{
	public class DrugstoreModel
	{

		public int DrugstoreId { get; set; }

		[StringLength(200)]
		public String DrugstoreName { get; set; } = String.Empty;

		[StringLength(200)]
		public String DrugstoreNameAscii { get; set; } = String.Empty;

		public int AddressId { get; set; }

		[Required]
		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		public int ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }

	}
}
