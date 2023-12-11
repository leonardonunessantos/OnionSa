namespace OnionSa.Core.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

    }
}
