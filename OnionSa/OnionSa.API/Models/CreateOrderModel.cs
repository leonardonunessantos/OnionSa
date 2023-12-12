namespace OnionSa.API.Models;
// Futuramente criar padrão de entrada de dados, não a leitura direta do excel
public class CreateOrderModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Product { get; set; }
    public DateTime CreatedDate { get; set; }
}
