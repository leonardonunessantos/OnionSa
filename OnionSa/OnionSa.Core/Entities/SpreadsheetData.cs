namespace OnionSa.Core.Entities;

public class SpreadsheetData
{
    public SpreadsheetData(string documento, string razaoSocial, string cEP, string produto, int numeroPedido, DateTime data)
    {
        Documento = documento;
        RazaoSocial = razaoSocial;
        CEP = cEP;
        Produto = produto;
        NumeroPedido = numeroPedido;
        Data = data;
    }

    public string Documento { get; set; }
    public string RazaoSocial { get; set; }
    public string CEP { get; set; }
    public string Produto { get; set; }
    public int NumeroPedido { get; set; }
    public DateTime Data { get; set; }
}
