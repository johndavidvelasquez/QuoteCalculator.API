using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal InterestRate { get; set; }
        public int MinMonth { get; set; }
        public int MaxMonth { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int MonthInterestDiscount { get; set; }
        public ICollection<ProductFee> ProductFees { get; set; }
    }
}
