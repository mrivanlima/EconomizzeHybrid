using EconomizzeHybrid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Interfaces
{
	internal interface IPrescriptionServices
	{
		PrescriptionModel Prescription { get; set; }
		bool isError { get; set; }
		Task CreatePrescriptionAsync(PrescriptionModel prescription);
		Task CreateQuotePrescriptionAsync(QuotePrescriptionModel quotePrescriptionModel);
	}
}
