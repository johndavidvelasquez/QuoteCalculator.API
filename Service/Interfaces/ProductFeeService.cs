
using DataAccess.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public class ProductFeeService : IProductFeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductFeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductFee> GetProductFeesByProductId(int productId)
        {
            IEnumerable<ProductFee> productFees = new List<ProductFee>();
            productFees = _unitOfWork.ProductFees.GetAll().Where(x => x.IsArchived == false && x.ProductId == productId).ToList();


            return productFees;
        }
    }
}
