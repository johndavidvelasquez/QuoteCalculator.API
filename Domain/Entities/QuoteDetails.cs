using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuoteDetails
    {
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public decimal? TotalFees { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? Repayment { get; set; }
    }
}
