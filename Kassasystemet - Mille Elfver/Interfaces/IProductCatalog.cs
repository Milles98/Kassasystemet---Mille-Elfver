using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver.Interfaces
{
    public interface IProductCatalog
    {
        void DataSeeding();
        Dictionary<string, Product> GetProducts();
    }
}
