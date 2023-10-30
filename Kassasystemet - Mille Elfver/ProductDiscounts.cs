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
        public decimal PercentageDiscount { get; set; }
        public DateTime DiscountStartDate { get; set; }
        public DateTime DiscountEndDate { get; set; }

        public ProductDiscount(decimal percentageDiscount, DateTime startDate, DateTime endDate)
        {
            PercentageDiscount = percentageDiscount;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }

        public ProductDiscount(decimal discount, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            PercentageDiscount = discount;
            BuyQuantity = buyQuantity;
            PayForQuantity = payForQuantity;
            DiscountStartDate = startDate;
            DiscountEndDate = endDate;
        }

    }
}
