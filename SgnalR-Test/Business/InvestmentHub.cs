using Microsoft.AspNetCore.SignalR;
using SgnalR_Test.Business;
using System.Xml.Linq;

public class InvestmentHub : Hub
{
    public async Task UpdateList(Response updatedList)
    {
        // Tüm bağlı istemcilere güncellenmiş listeyi gönder
        await Clients.All.SendAsync("GetRates", updatedList);
    }
}