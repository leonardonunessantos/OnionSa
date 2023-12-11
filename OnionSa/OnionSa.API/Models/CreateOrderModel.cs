namespace OnionSa.API.Models;

public class CreateOrderModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Product { get; set; }
    public DateTime CreatedDate { get; set; }
}
