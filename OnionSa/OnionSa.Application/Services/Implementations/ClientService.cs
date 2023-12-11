using Microsoft.Extensions.Configuration;
using OnionSa.Application.InputModels;
using OnionSa.Application.Services.Interfaces;
using OnionSa.Core.Entities;
using System.Globalization;

namespace OnionSa.Application.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly Infrastructure.Persistence.OnionSaDbContext _dbContext;
        private readonly string _connectionString;

        public ClientService(Infrastructure.Persistence.OnionSaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("OnionSaCs");
        }
        public bool Create(NewClientInputModel inputModel)
        {
            try
            {
                var existingClient = _dbContext.Clients.FirstOrDefault(c => c.Document == inputModel.Document);

                if (existingClient == null)
                {
                    var client = new Client(inputModel.Document, inputModel.Name, inputModel.Cep);
                    _dbContext.Clients.Add(client);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
}
