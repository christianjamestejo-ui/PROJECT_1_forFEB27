using System;
using System.Collections.Generic;

namespace LISTTCLASS
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            products.AddRange
            ([
                new Product()
                {
                    Name = "Piatos",
                    Id = 1,
                    Price = (decimal)10.50,
                    CreatedDate = DateTime.Now
                },
                new Product()
                {
                    Name = "Nova",
                    Id = 2,
                    Price = (decimal)11.50,
                    CreatedDate = DateTime.Now
                },
                new Product()
                {
                    Name = "Mang Juan",
                    Id = 3,
                    Price = (decimal)10,
                    CreatedDate = DateTime.Now
                }
            ]);

            foreach (var product in products)
            {
                Console.WriteLine($"\nName : {product.Name}\n" +
                                  $"ID : {product.Id} \n" +
                                  $"Price : {product.Price} \n" +
                                  $"Created Date : {product.CreatedDate.ToShortDateString()}");
            }
        }
    }
}
