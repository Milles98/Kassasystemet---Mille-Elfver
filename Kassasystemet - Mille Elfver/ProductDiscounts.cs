using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductDiscount
    {
        public decimal Discount { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }

        public ProductDiscount(decimal discount, DateTime startDate, DateTime endDate)
        {
            Discount = discount;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }
    }
}
