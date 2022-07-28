using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.DataAccess.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Iphone 12 256GB",
                    CategoryId = 1,
                    Price = 14200
                },
                new Product
                {
                    Id=2,
                    Name = "S21 FE 256GB",
                    CategoryId =1,
                    Price = 10999
                },
                new Product
                {
                    Id=3,
                    Name = "Apple MacBook Air",
                    CategoryId = 2,
                    Price = 35999
                });
        }
    }
}
