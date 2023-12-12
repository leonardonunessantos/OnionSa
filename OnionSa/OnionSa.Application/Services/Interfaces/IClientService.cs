using OnionSa.Application.InputModels;
using OnionSa.Core.Entities;

namespace OnionSa.Application.Services.Interfaces;

public interface IClientService
{
    bool CreateAll(List<Dictionary<string, string>> listInputModel);
    List<NewClientInputModel> CreateList(List<Dictionary<string, string>> dataAsList);
}
