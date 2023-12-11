using OnionSa.Core.Entities;

namespace OnionSa.Infrastructure.Persistence.Configurations
{
    public static class SeedData
    {
        public static void Initialize(OnionSaDbContext context)
        {
            context.Products.AddRange(
                new Product
                {
                    Id = -1,
                    Name = "Celular",
                    Value = 1000m
                },
                new Product
                {
                    Id = -2,
                    Name = "Notebook",
                    Value = 3000m
                },
                new Product
                {
                    Id = -3,
                    Name = "Televisão",
                    Value = 5000m
                }
            //... outros produtos
            );

            context.SaveChanges();
        }
    }
}
