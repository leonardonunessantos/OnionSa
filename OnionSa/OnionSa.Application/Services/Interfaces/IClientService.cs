using OnionSa.Application.InputModels;

namespace OnionSa.Application.Services.Interfaces;

public interface IClientService
{
    bool Create(NewClientInputModel inputModel);
    List<NewClientInputModel> CreateList(List<Dictionary<string, string>> dataAsList);
}
