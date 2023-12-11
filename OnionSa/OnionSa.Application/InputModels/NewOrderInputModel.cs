namespace OnionSa.Application.InputModels;

public class NewOrderInputModel
{
    public NewOrderInputModel(int orderId, string productName, DateTime createdAt, string documentClient, string cep, string razaoSocial)
    {
        OrderId = orderId;
        ProductName = productName;
        CreatedAt = createdAt;
        DocumentClient = documentClient;
        Cep = cep;
        RazaoSocial = razaoSocial;
    }

    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DocumentClient { get; set; }
    public string Cep { get; set; }
    public string RazaoSocial { get; set; }
}
