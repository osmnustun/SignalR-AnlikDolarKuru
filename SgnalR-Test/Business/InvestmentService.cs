using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net;
namespace SgnalR_Test.Business
{
    public class InvestmentService : BackgroundService
    {
        readonly HttpClient _httpClient;
        private readonly IHubContext<InvestmentHub> _hubContext;

        public InvestmentService(IHubContext<InvestmentHub> hubContext, HttpClient httpClient)
        {
            _hubContext = hubContext;
            _httpClient = httpClient;
        }
       
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var rates = await GetRates();
                await _hubContext.Clients.All.SendAsync("GetRates", rates);
            }
        }

        public async Task<Response> GetRates()
        {
            var content = await _httpClient.GetStringAsync(new Uri("http://hasanadiguzel.com.tr/api/kurgetir"));
            var response = JsonConvert.DeserializeObject<Response>(content);

            return response;
        }


    }
}
