using Microsoft.AspNetCore.Http;

namespace OnionSa.Application.Services.Interfaces;

public interface ISpreadsheetService
{
    Task<List<Dictionary<string, string>>> ProcessExcelFiles(ICollection<IFormFile> files);
}
