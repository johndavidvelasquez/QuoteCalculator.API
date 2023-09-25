using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            LoanApplications = new LoanApplicationRepository(_context);
            Products = new ProductRepository(_context);
            ProductFees = new ProductFeeRepository(_context);
            BlackList = new BlackListRepository(_context);
        }

        public ILoanApplicationRepository LoanApplications { get; private set; }
        public IProductRepository Products { get; private set; }
        public IProductFeeRepository ProductFees { get; private set; }
        public IBlackListRepository BlackList { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
