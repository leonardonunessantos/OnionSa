using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionSa.Application.InputModels;
using OnionSa.Application.Services.Interfaces;
using OnionSa.Application.ViewModels;
using OnionSa.Core.Entities;

namespace OnionSa.Application.Services.Implementations;

public class ClientService : IClientService
{
    private readonly Infrastructure.Persistence.OnionSaDbContext _dbContext;
    private readonly string _connectionString;

    public ClientService(Infrastructure.Persistence.OnionSaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("OnionSaCs");
    }
    public bool CreateAll(List<Dictionary<string, string>> listInputModel)
    {
        try
        {
            var listClients = CreateList(listInputModel);

            foreach (var item in listClients)
            {
                var existingClient = _dbContext.Clients.FirstOrDefault(c => c.Document == item.Document);

                if (existingClient == null)
                {
                    var client = new Client(item.Document, item.Name, item.Cep);
                    _dbContext.Clients.Add(client);
                    _dbContext.SaveChanges();
                }
            }
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public List<NewClientInputModel> CreateList(List<Dictionary<string, string>> dataAsList)
    {
        List<NewClientInputModel> clients = new List<NewClientInputModel>();
        foreach (var item in dataAsList)
        {
            var document = item["Documento"];
            var cep = item["CEP"];
            var razaoSocial = item["Razão social"];
            NewClientInputModel clientInputModel = new NewClientInputModel(razaoSocial, document, cep);
            clients.Add(clientInputModel);
        }
        return clients;
    }
}
