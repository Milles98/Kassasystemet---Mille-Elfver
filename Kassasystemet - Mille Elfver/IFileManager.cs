using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public interface IFileManager
    {
        public void SaveProductsToFile(Dictionary<string, Product> availableProducts);
        public void LoadProductsFromFile(Dictionary<string, Product> availableProducts);
        public void SaveReceiptCounter(int receiptCounter);
        public int LoadReceiptCounter();
        public void SaveReceipt(string receiptText);
    }
}
