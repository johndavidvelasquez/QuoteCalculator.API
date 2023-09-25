using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILoanApplicationService
    {
        LoanApplication GetLoanApplicationByBasicDetails(string firstName, string lastName, DateTime dateOfBirth);
        LoanApplication GetLoanApplicationById(int id);
        LoanApplication Add(LoanApplication application);
        LoanApplication Update(LoanApplication application);
        QuoteDetails CalculateQuoteDetails(int productId, int term, decimal amountRequired);
        bool ValidateDomain(string email);
        bool ValidateMobile(string mobile);
    }
}
