using OnionSa.Application.InputModels;
using OnionSa.Application.ViewModels;

namespace OnionSa.Application.Services.Interfaces;

public interface IOrderService
{
    List<OrderViewModel> GetAll();
    bool CreateAll(List<Dictionary<string, string>> listInputModel);
    List<NewOrderInputModel> CreateList(List<Dictionary<string, string>> dataAsList);
    Task<int> EstimateDeliveryTime(string cep, string region);
    Task<decimal> CalculateFreteCost(string cep, string product, string region);
    Task<string> GetRegion(string cep);
}
