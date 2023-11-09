using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductDictionary
    {
        private static ProductDictionary _instance;
        private Dictionary<string, Product> _products;
        private FileManager _fileManager;

        private ProductDictionary()
        {
            _products = new Dictionary<string, Product>();
            _fileManager = new FileManager();
            DataSeeding();
        }
        public static ProductDictionary Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductDictionary();
                }
                return _instance;
            }
        }

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
            AddProductToDictionary("300", "Bananer", 0, 15.50m);
            AddProductToDictionary("301", "Tomater", 0, 79.90m);
            AddProductToDictionary("302", "Apelsiner", 0, 9.90m);
            AddProductToDictionary("303", "Godis", 0, 99.50m);
            AddProductToDictionary("304", "Kött", 0, 199m);
            AddProductToDictionary("305", "Kyckling", 0, 129.90m);
            AddProductToDictionary("306", "Nutella", 21.90m, 0);
            AddProductToDictionary("307", "Citron", 5.50m, 0);
            AddProductToDictionary("308", "Jordgubbar", 39.90m, 0);
            AddProductToDictionary("309", "Grädde", 24.90m, 0);
            AddProductToDictionary("310", "Choklad", 22.90m, 0);
            AddProductToDictionary("311", "Mango", 19.90m, 0);
            AddProductToDictionary("312", "Öl", 15.50m, 0);
            AddProductToDictionary("313", "Kex", 23.90m, 0);
            AddProductToDictionary("314", "Gurka", 10m, 0);
            AddProductToDictionary("315", "Leverpastej", 24.90m, 0);
            AddProductToDictionary("316", "Chips", 27.90m, 0);
            AddProductToDictionary("317", "Bröd", 34.90m, 0);
            AddProductToDictionary("318", "Mjölk", 16.90m, 0);
            AddProductToDictionary("319", "Yoghurt", 29.90m, 0);
            AddProductToDictionary("320", "Glass", 39.90m, 0);
        }

        /// <summary>
        /// Adds products to the dictionary
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        private void AddProductToDictionary(string productId, string name, decimal unitPrice, decimal kiloPrice)
        {
            if (_products.ContainsKey(productId))
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
