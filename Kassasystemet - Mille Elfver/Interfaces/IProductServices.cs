using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver.Interfaces
{
    public interface IProductServices
    {
        void SetPercentageDiscount(string productId, decimal percentage, DateTime startDate, DateTime endDate);
        void SetQuantityDiscount(string productId, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate);
        void RemoveDiscount(string productId);
        void UpdateProductName(string productId, string newName);
        void UpdateProductPrice(string productId, decimal newPrice, string priceType);
        void AddProductWithPriceType(string productId, string productName, string priceType, decimal price);
        void RemoveProduct(string productId);
        bool ProductExists(string productId);
        Product GetProduct(string productId);
        void AddProduct(string productId, string productName, decimal unitPrice, decimal kiloPrice, string priceType);
        void DisplayAvailableProducts();
    }
}
