using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class ProductFeeRepository : GenericRepository<ProductFee>, IProductFeeRepository
    {
        public ProductFeeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
