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
	internal class QuoteServices : IQuoteServices
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MessageHandler _messageHandler;
		private JsonSerializerOptions options { get; set; }
		public QuoteModel? Quote { get; set; }
		public bool isError { get; set; }

		public QuoteServices(IHttpClientFactory httpClientFactory, MessageHandler messageHandler)
		{
			_httpClientFactory = httpClientFactory;
			_messageHandler = messageHandler;
			options = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				PropertyNameCaseInsensitive = true
			};
		}

		#region CREATE QUOTE
		public async Task CreateQuoteAsync(QuoteModel quote)
		{
			var url = $"orcamento/criar";
			try
			{
				var httpClient = _httpClientFactory.CreateClient("economizze");
				var response = await httpClient.PostAsJsonAsync(url, quote);
				var jsonResponse = await response.Content.ReadAsStringAsync();


				if (response.IsSuccessStatusCode)
				{
					isError = false;
					Quote = JsonSerializer.Deserialize<QuoteModel>(jsonResponse, options);
					_messageHandler.Message = "Sucesso!";
				}
				else
				{
					isError = true;
					Quote = null;
					_messageHandler.Message = jsonResponse.ToString();
				}
			}
			catch (Exception ex)
			{
				_messageHandler.Message = ex.Message;
			}
		}
		#endregion

		#region FIND NEIGHBORHOOD ID
		public async Task FindNeighborhoodId(int streetId)
		{
			var url = $"orcamento/street/{streetId}";
			try
			{
				var httpClient = _httpClientFactory.CreateClient("economizze");
				var response = await httpClient.GetAsync(url);
				var jsonResponse = await response.Content.ReadAsStringAsync();


				if (response.IsSuccessStatusCode)
				{
					isError = false;
					Quote = JsonSerializer.Deserialize<QuoteModel>(jsonResponse, options);
					//_messageHandler.Message = "Sucesso!";
				}
				else
				{
					isError = true;
					Quote = null;
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
