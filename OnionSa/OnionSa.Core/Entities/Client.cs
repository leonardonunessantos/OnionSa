namespace OnionSa.Core.Entities;

public class Client : BaseEntity
{
    public Client(string document, string name, string cep)
    {
        Document = document;
        Name = name;
        Cep = cep;
    }

    public int Id { get; private set; }
    public string Document { get; private set; }
    public string Name { get; private set; }
    public string Cep { get; private set; }
    public List<Order> Orders { get; private set; }

    //public void setOrder (Order order)
    //{
    //    if (order != null)
    //    {
    //        Orders.Add (order);
    //    }
    //}
}
