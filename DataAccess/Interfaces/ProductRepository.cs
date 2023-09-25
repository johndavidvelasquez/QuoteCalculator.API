using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Product GetProductWithProductFees(int productId)
        {
            return _context.Products.Include(p => p.ProductFees).FirstOrDefault(x => x.Id == productId && x.IsArchived == false);
        }
    }
}
