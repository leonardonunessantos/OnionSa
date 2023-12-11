using OnionSa.Application.InputModels;
using OnionSa.Application.Services.Interfaces;
using OnionSa.Application.ViewModels;
using OnionSa.Application.Services.Helpers;
using OnionSa.Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionSa.Core.Entities;
using System.Globalization;

namespace OnionSa.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IViaCepService _viaCepService;
        private readonly RegionMapper _regionMapper;
        private readonly Infrastructure.Persistence.OnionSaDbContext _dbContext;
        private readonly string _connectionString;

        public OrderService(IViaCepService viaCepService, RegionMapper regionMapper, Infrastructure.Persistence.OnionSaDbContext dbContext, IConfiguration configuration)
        {
            _viaCepService = viaCepService;
            _regionMapper = regionMapper;
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("OnionSaCs");
        }
        public bool Create(NewOrderInputModel inputModel)
        {
            try
            {
                int totalDaysFrete = EstimateDeliveryTime(inputModel.Cep).Result;
                decimal totalFreteCost = CalculateFreteCost(inputModel.Cep, inputModel.ProductName).Result;
                var order = new Order(inputModel.OrderId, inputModel.DocumentClient, inputModel.Cep, inputModel.ProductName, inputModel.CreatedAt, totalFreteCost, totalDaysFrete);
                var client = _dbContext.Clients.FirstOrDefault(c => c.Document == inputModel.DocumentClient);
                var product = _dbContext.Products.FirstOrDefault(p => p.Name == inputModel.ProductName);
                //client.setOrder(order);
                order.SetClient(client);
                order.SetProduct(product);
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public List<OrderViewModel> GetAll()
        {
            var orderCount = _dbContext.Orders.Count();
            var clientCount = _dbContext.Clients.Count();
            var productCount = _dbContext.Products.Count();
            var orders = _dbContext.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .ToList();
            var ordersViewModel = orders
                .Select(o => new OrderViewModel(o.OrderId, o.Client.Name, o.Client.Cep, o.ProductName, o.Product.Value, o.TotalCostFrete, o.TotalDaysFrete, o.CreatedAt))
                .ToList();
            return ordersViewModel;
        }

        public List<NewOrderInputModel> CreateList(List<Dictionary<string, string>> dataAsList)
        {
            List<NewOrderInputModel> orders = new List<NewOrderInputModel>();
            foreach (var item in dataAsList)
            {
                var orderId = Convert.ToInt32(item["Número do pedido"]);
                var productName = item["Produto"];
                var date = DateTime.ParseExact(item["Data"], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var document = item["Documento"];
                var cep = item["CEP"];
                var razaoSocial = item["Razão social"];
                NewOrderInputModel orderInputModel = new NewOrderInputModel(orderId, productName, date, document, cep, razaoSocial);
                orders.Add(orderInputModel);
            }
            return orders;
        }

        public async Task<int> EstimateDeliveryTime(string cep)
        {
            ViaCepResponse viaCepResponse = await _viaCepService.GetLocationByCepAsync(cep);
            //Dictionary<string, string> viaCepResponse = new Dictionary<string, string>;
            //{
            //{"cep", "03656-000"},
            //{"logradouro", "Rua Guaranésia"},
            //{"complemento", ""},
            //{"bairro", "Vila Ré"},
            //{"localidade", "São Paulo"},
            //{"uf", "SP"},
            //{"ibge", "3550308"},
            //{"gia", "1004"},
            //{"ddd", "11"},
            //{"siafi", "7107"}
            //};
            if (viaCepResponse != null)
            {
                string region = _regionMapper.GetRegionByState(viaCepResponse.Uf);
                return CalculateDeliveryTime(viaCepResponse.Uf, viaCepResponse.Localidade, region);
                //string region = _regionMapper.GetRegionByState(viaCepResponse["uf"]);
                //return CalculateDeliveryTime(viaCepResponse["uf"], viaCepResponse["Localidade"], region);
            }
            else
            {
                return -1;
            }
        }

        public async Task<decimal> CalculateFreteCost(string cep, string product)
        {
            ViaCepResponse viaCepResponse = await _viaCepService.GetLocationByCepAsync(cep);
            if (viaCepResponse != null)
            {
                string region = _regionMapper.GetRegionByState(viaCepResponse.Uf);
                return CalculateShippingCost(viaCepResponse.Uf, viaCepResponse.Localidade, region, product);
            }
            else
            {
                return -1;
            }
        }

        public int CalculateDeliveryTime(string stateCode, string city, string region)
        {
            if (stateCode == "SP" && city == "São Paulo")
            {
                return 0;
            }
            else
            {
                switch (region.ToLower())
                {
                    case "norte":
                    case "nordeste":
                        return 10;
                    case "centro-oeste":
                    case "sul":
                        return 5;
                    case "sudeste":
                        return 1;
                    default:
                        return -1;
                }
            }
        }

        public decimal CalculateShippingCost(string stateCode, string city, string region, string product)
        {
            decimal productValue = 0;

            switch (product.ToLower())
            {
                case "celular":
                    productValue = 1000;
                    break;
                case "notebook":
                    productValue = 3000;
                    break;
                case "televisão":
                    productValue = 5000;
                    break;
                default:
                    return -1;
            }

            decimal totalCost = 0;

            if (stateCode == "SP" && city == "São Paulo")
            {
                return totalCost;
            }
            else
            {

                switch (region.ToLower())
                {
                    case "norte":
                    case "nordeste":
                        totalCost = productValue * 0.3m;
                        break;
                    case "centro-oeste":
                    case "sul":
                        totalCost = productValue * 0.2m;
                        break;
                    case "sudeste":
                        totalCost = productValue * 0.1m;
                        break;
                    default:
                        return -1;
                }
                return totalCost;
            }
        }
    }
}
