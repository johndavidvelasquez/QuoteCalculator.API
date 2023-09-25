using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILoanApplicationRepository LoanApplications { get; }
        IProductRepository Products { get; }
        IProductFeeRepository ProductFees { get; }
        IBlackListRepository BlackList { get; }
        int Complete();
    }
}
