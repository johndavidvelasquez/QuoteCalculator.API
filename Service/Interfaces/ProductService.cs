
using DataAccess.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            IEnumerable<Product> products = new List<Product>();    
            products = _unitOfWork.Products.GetAll().Where(x => x.IsArchived == false).ToList();

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = new Product();    
            product = _unitOfWork.Products.GetProductWithProductFees(productId);

            return product;
        }
    }
}
