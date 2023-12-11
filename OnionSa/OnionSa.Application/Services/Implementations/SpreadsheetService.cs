using OnionSa.Application.Services.Interfaces;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using OnionSa.Core.Entities;
using System.Globalization;
using System.Text;

public class SpreadsheetService : ISpreadsheetService
{
    public async Task<List<Dictionary<string, string>>> ProcessExcelFiles(ICollection<IFormFile> files)
    {
        var expectedColumns = new List<string>
        {
            "Documento",
            "Razão Social",
            "CEP",
            "Produto",
            "Número do pedido",
            "Data"
        };
        var dataAsList = new List<Dictionary<string, string>>();

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet != null)
                        {
                            int rowCount = worksheet.Dimension.Rows;
                            int colCount = worksheet.Dimension.Columns;

                            var columnTitles = new List<string>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                columnTitles.Add(worksheet.Cells[1, col].Value?.ToString());
                            }

                            // Validando titulos
                            if (!expectedColumns.All(title =>
                                columnTitles.Any(columnTitle => columnTitle == title)))
                            {
                                throw new InvalidOperationException("Os títulos das colunas não estão no padrão esperado: Documento, Razão social, Cep, Produto, Numero do pedido e Data");
                            }

                            for (int row = 2; row <= rowCount; row++) // Começa na segunda linha para ignorar os titulos
                            {
                                // tratando a data:
                                var date = worksheet.Cells[row, 6].Value?.ToString();
                                DateTime dataConvertida = new DateTime(1900, 01, 01);
                                if (double.TryParse(date, out double numeroSerial))
                                {
                                    dataConvertida = DateTime.FromOADate(numeroSerial);
                                }
                                var rowData = new Dictionary<string, string>
                                {
                                    { "Documento", worksheet.Cells[row, 1].Value?.ToString().Replace(".", "").Replace("-", "") },
                                    { "Razão social", worksheet.Cells[row, 2].Value?.ToString() },
                                    { "CEP", worksheet.Cells[row, 3].Value?.ToString().Replace("-", "") },
                                    { "Produto", worksheet.Cells[row, 4].Value?.ToString() },
                                    { "Número do pedido", worksheet.Cells[row, 5].Value?.ToString() },
                                    { "Data", dataConvertida.ToString() }
                                };

                                // Verifica se todos os valores da linha são nulos ou vazios para poder encerrar
                                if (rowData.Where(rw => rw.Key != "Data").All(rw => string.IsNullOrWhiteSpace(rw.Value)))
                                {
                                    break;
                                }

                                dataAsList.Add(rowData);
                            }
                        }
                    }
                }
            }
        }
        return dataAsList;
    }
}

