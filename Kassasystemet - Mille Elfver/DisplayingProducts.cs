using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class DisplayingProducts
    {
        public static void DisplayTheProducts()
        {
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in Product.availableProducts)
            {
                Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.UnitPrice} {product.Value.PriceType})\n");
            }
        }
    }
}
