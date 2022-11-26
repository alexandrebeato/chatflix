using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stock.Service.Interfaces;
using Stock.Service.Models;

namespace Stock.Service.Services
{
    public class StooqService : IStooqService
    {
        protected readonly HttpClient _httpClient;

        public StooqService(HttpClient httpClient, IConfiguration configuration)
        {
            var endPoint = configuration["stooq:uri"];

            if (string.IsNullOrEmpty(endPoint))
                throw new ArgumentNullException(nameof(endPoint));

            _httpClient = ConfigureHttpClient(httpClient, endPoint);
        }

        private HttpClient ConfigureHttpClient(HttpClient httpClient, string apiEndpoint)
        {
            httpClient.BaseAddress = new Uri(apiEndpoint);
            return httpClient;
        }

        public async Task<StooqResult?> GetStocks(string symbol)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.GetAsync($"q/l/?s={symbol}&f=sd2t2ohlcv&h&e=json");

            if (!response.IsSuccessStatusCode)
                return null;

            return JsonConvert.DeserializeObject<StooqResult>(await response.Content.ReadAsStringAsync());
        }
    }
}