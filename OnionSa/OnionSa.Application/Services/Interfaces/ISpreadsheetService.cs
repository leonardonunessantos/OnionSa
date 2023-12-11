using Microsoft.AspNetCore.Http;
using OnionSa.Core.Entities;

namespace OnionSa.Application.Services.Interfaces
{
    public interface ISpreadsheetService
    {
        Task<List<Dictionary<string, string>>> ProcessExcelFiles(ICollection<IFormFile> files);
    }
}
