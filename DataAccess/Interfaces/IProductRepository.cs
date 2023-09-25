﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetProductWithProductFees(int productId);
    }
}
