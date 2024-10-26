using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EconomizzeHybrid.Services.Classes
{
	internal class PrescriptionServices : IPrescriptionServices
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MessageHandler _messageHandler;
		private JsonSerializerOptions options { get; set; }
		public PrescriptionModel? Prescription { get; set; }
		public bool isError { get; set; }

		public PrescriptionServices(IHttpClientFactory httpClientFactory,
									MessageHandler messageHandler)
		{
			_httpClientFactory = httpClientFactory;
			_messageHandler = messageHandler;
			options = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				PropertyNameCaseInsensitive = true
			};
		}

		#region CREATE PRESCRIPTION
		public async Task CreatePrescriptionAsync(PrescriptionModel prescription)
		{
			var url = $"prescricao/criar";
			try
			{
				var httpClient = _httpClientFactory.CreateClient("economizze");
				var response = await httpClient.PostAsJsonAsync(url, prescription);
				var jsonResponse = await response.Content.ReadAsStringAsync();


				if (response.IsSuccessStatusCode)
				{
					isError = false;
					Prescription = JsonSerializer.Deserialize<PrescriptionModel>(jsonResponse, options);
					_messageHandler.Message = "Prescricao Adicionado!";
				}
				else
				{
					isError = true;
					Prescription = null;
					_messageHandler.Message = jsonResponse.ToString();
				}
			}
			catch (Exception ex)
			{
				_messageHandler.Message = ex.Message;
			}
		}
		#endregion

		#region CREATE QUOTE PRESCRIPTION
		public async Task CreateQuotePrescriptionAsync(QuotePrescriptionModel quotePrescriptionModel)
		{
			var url = $"prescricao/criar/orcamentoprescricao";
			try
			{
				var httpClient = _httpClientFactory.CreateClient("economizze");
				var response = await httpClient.PostAsJsonAsync(url, quotePrescriptionModel);
				var jsonResponse = await response.Content.ReadAsStringAsync();


				if (response.IsSuccessStatusCode)
				{
					isError = false;
					Prescription = JsonSerializer.Deserialize<PrescriptionModel>(jsonResponse, options);
					_messageHandler.Message = "Prescricao Adicionado!";
				}
				else
				{
					isError = true;
					Prescription = null;
					_messageHandler.Message = jsonResponse.ToString();
				}
			}
			catch (Exception ex)
			{
				_messageHandler.Message = ex.Message;
			}
		}
		#endregion
	}
}
