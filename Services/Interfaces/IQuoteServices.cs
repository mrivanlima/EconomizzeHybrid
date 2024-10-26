using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
	public interface IQuoteServices
	{
		QuoteModel Quote { get; set; }
		bool isError { get; set; }
		Task CreateQuoteAsync(QuoteModel quote);
		Task FindNeighborhoodId(int streetId);
	}
}
