using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductCatalog : IProductCatalog
    {
        private static IProductCatalog _instance = new ProductCatalog();
        private Dictionary<string, Product> _products;
        private IFileManager _fileManager;

        private ProductCatalog()
        {
            _products = new Dictionary<string, Product>();
            _fileManager = new FileManager();
            DataSeeding();
        }

        public static IProductCatalog Instance => _instance;

        public void DataSeeding()
        {
            _fileManager.LoadProductsFromFile(_products);

            if (_products.Count == 0)
            {
                StartingItems();
                _fileManager.SaveProductsToFile(_products);
            }
        }

        /// <summary>
        /// Contains all the pre-defined products for the cashregister
        /// </summary>
        private void StartingItems()
        {
            AddProductToCatalog("300", "Bananer", 0, 15.50m);
            AddProductToCatalog("301", "Tomater", 0, 79.90m);
            AddProductToCatalog("302", "Apelsiner", 0, 9.90m);
            AddProductToCatalog("303", "Godis", 0, 99.50m);
            AddProductToCatalog("304", "Kött", 0, 199m);
            AddProductToCatalog("305", "Kyckling", 0, 129.90m);
            AddProductToCatalog("306", "Nutella", 21.90m, 0);
            AddProductToCatalog("307", "Citron", 5.50m, 0);
            AddProductToCatalog("308", "Jordgubbar", 39.90m, 0);
            AddProductToCatalog("309", "Grädde", 24.90m, 0);
            AddProductToCatalog("310", "Choklad", 22.90m, 0);
            AddProductToCatalog("311", "Mango", 19.90m, 0);
            AddProductToCatalog("312", "Öl", 15.50m, 0);
            AddProductToCatalog("313", "Kex", 23.90m, 0);
            AddProductToCatalog("314", "Gurka", 10m, 0);
            AddProductToCatalog("315", "Leverpastej", 24.90m, 0);
            AddProductToCatalog("316", "Chips", 27.90m, 0);
            AddProductToCatalog("317", "Bröd", 34.90m, 0);
            AddProductToCatalog("318", "Mjölk", 16.90m, 0);
            AddProductToCatalog("319", "Yoghurt", 29.90m, 0);
            AddProductToCatalog("320", "Glass", 39.90m, 0);
        }

        /// <summary>
        /// Adds products to the dictionary
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        private void AddProductToCatalog(string productId, string name, decimal unitPrice, decimal kiloPrice)
        {
            if (_products.ContainsKey(productId)) // om ID redan finns ska en ny product ej skapas
            {
                _products[productId].Name = name;
                _products[productId].UnitPrice = unitPrice;
                _products[productId].KiloPrice = kiloPrice;
            }
            else
            {
                var product = new Product(productId, name, unitPrice, kiloPrice);
                _products.Add(product.Id, product);
            }
        }
        public Dictionary<string, Product> GetProducts()
        {
            return _products;
        }
    }
}
