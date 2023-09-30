﻿using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Product
    {

        //auto implemented properties (typ som instansvariabler men fixade i bakgrunden)
        public string Name { get; }
        public decimal Price { get; }
        public string PriceType { get; }

        //name är ex bananer, price ex 15.50m, priceType ex kg
        //konstruktor
        public Product(string name, decimal price, string priceType)
        {
            Name = name;
            Price = price;
            PriceType = priceType;
        }


        public static readonly Dictionary<string, Product> availableProducts = new Dictionary<string, Product>
                {
            { "300", new Product("Bananer", 15.50m, " kg") },
            { "301", new Product("Nutella", 21.90m, " st") },
            { "302", new Product("Citron", 5.50m, " kg") },
            { "303", new Product("Jordgubbar", 39.90m, " kg") },
            { "304", new Product("Grädde", 24.90m, " st") },
            { "305", new Product("Choklad", 22.90m, " st") },
            { "306", new Product("Apelsiner", 9.90m, " st") },
            { "307", new Product("Mango", 19.90m, " st") },
            { "308", new Product("Tomater", 49.90m, " st") },
            { "309", new Product("Kött", 199m, " st") },
            { "310", new Product("Godis", 99.50m, " kg") }
                };

        /// <summary>
        /// Shows available products in dictionary to user
        /// </summary>
        public static void DisplayTheProducts()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in Product.availableProducts)
            {
                Console.WriteLine($"{product.Key}:{product.Value.Name}({product.Value.Price} {product.Value.PriceType})\n");
                //Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.Price} {product.Value.PriceType})\n");
            }
        }
    }
}
