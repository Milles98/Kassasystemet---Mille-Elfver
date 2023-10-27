using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductDiscount
    {
        public int BuyQuantity { get; set; }
        public int PayForQuantity { get; set; }
        public decimal Discount { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }

        public ProductDiscount(decimal discount, DateTime startDate, DateTime endDate)
        {
            Discount = discount;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }
        public ProductDiscount(int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            BuyQuantity = buyQuantity;
            PayForQuantity = payForQuantity;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }

        public ProductDiscount(decimal discount, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            Discount = discount;
            BuyQuantity = buyQuantity;
            PayForQuantity = payForQuantity;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }

    }
}
