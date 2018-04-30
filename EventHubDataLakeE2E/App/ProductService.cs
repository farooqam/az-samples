using System;
using System.Collections.Generic;

namespace App
{
    public class ProductService
    {
        private readonly List<Product> _products = new List<Product>();

        public ProductService()
        {
            for (var i = 0; i < 100; i++)
            {
                _products.Add(new Product {Id = $"Product_{i + 1}"});
            }
        }

        public Product GetProduct()
        {
            var random = new Random();
            return _products[random.Next(0, _products.Count - 1)];
        }
    }
}