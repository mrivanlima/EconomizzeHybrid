using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EconomizzeHybrid.Model
{
	public class QuoteModel
	{
		public long QuoteId { get; set; }

		public int UserId { get; set; }

		public int NeighborhoodId { get; set; }

		public bool IsExpired { get; set; } = false;

		[Required]
		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		public int ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }

	}
}
