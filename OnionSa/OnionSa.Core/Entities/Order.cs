namespace OnionSa.Core.Entities;

    public class Order : BaseEntity
    {
        public Order(int orderId, string clientDocument, string cep, string productName, DateTime createdAt, decimal totalCostFrete, int totalDaysFrete, string region)
        {
            OrderId = orderId;
            ClientDocument = clientDocument;
            Cep = cep;
            ProductName = productName;
            CreatedAt = createdAt;
            TotalCostFrete = totalCostFrete;
            TotalDaysFrete = totalDaysFrete;
            Region = region;
        }

        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public string Cep { get; private set; }
        public string ClientDocument { get; private set; }
        public Client Client { get; private set; }
        public string ProductName { get; private set; }
        public string Region { get; private set; }
        public Product Product { get; private set; }
        public decimal TotalCostFrete { get; private set; }
        public int TotalDaysFrete { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void SetClient(Client client)
        {
            if (client != null)
            {
                this.Client = client;
            }
        }
        public void SetProduct(Product product)
        {
            if (product != null)
            {
                this.Product = product;
            }
        }
    }

