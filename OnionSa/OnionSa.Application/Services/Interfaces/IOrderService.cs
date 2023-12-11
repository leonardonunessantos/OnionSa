using OnionSa.Application.InputModels;
using OnionSa.Application.ViewModels;

namespace OnionSa.Application.Services.Interfaces;

public interface IOrderService
{
    List<OrderViewModel> GetAll();
    bool Create(NewOrderInputModel inputModel);
    List<NewOrderInputModel> CreateList(List<Dictionary<string, string>> dataAsList);
    Task<int> EstimateDeliveryTime(string cep);
    Task<decimal> CalculateFreteCost(string cep, string product);
}
