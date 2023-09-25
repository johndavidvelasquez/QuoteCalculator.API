using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class LoanApplicationRepository : GenericRepository<LoanApplication>, ILoanApplicationRepository
    {
        public LoanApplicationRepository(ApplicationDbContext context) : base(context)
        {

        }

        public LoanApplication Update(LoanApplication application)
        {
            var res = _context.LoanApplications.SingleOrDefault(x => x.Id == application.Id);
            if (res != null)
            {
                res.AmountRequired = application.AmountRequired;
                res.Term = application.Term;
                res.Title = application.Title;
                res.FirstName = application.FirstName;
                res.LastName = application.FirstName;
                res.DateOfBirth = application.DateOfBirth;
                res.Email = application.Email;
                res.Mobile = application.Mobile;
                res.LoanApplicationStatus = application.LoanApplicationStatus;
                res.ProductId = application.ProductId;

                _context.SaveChanges();
            }

            return res;
        }
    }
}
