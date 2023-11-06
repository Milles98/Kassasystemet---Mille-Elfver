using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductDiscount
    {
        public decimal PercentageDiscount { get; set; }
        public DateTime PercentStartDate { get; set; }
        public DateTime PercentEndDate { get; set; }
        public int BuyQuantity { get; set; }
        public int PayForQuantity { get; set; }
        public DateTime BuyQuantityStartDate { get; set; }
        public DateTime BuyQuantityEndDate { get; set; }

        public ProductDiscount(decimal percentageDiscount, DateTime startDate, DateTime endDate)
        {
            PercentageDiscount = percentageDiscount;
            PercentStartDate = startDate;
            PercentEndDate = endDate;
        }

        public ProductDiscount(decimal discount, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate,
            DateTime buyQuantityStartDate, DateTime buyQuantityEndDate)
        {
            PercentageDiscount = discount;
            BuyQuantity = buyQuantity;
            PayForQuantity = payForQuantity;
            PercentStartDate = startDate;
            PercentEndDate = endDate;
            BuyQuantityStartDate = buyQuantityStartDate;
            BuyQuantityEndDate = buyQuantityEndDate;
        }

    }
}
