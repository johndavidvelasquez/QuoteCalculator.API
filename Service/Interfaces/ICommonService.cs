using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICommonService
    {
        decimal CalculatePMT(decimal interestRate, int payments, decimal principal);
        bool ValidateAge(DateTime dateOfBirth);

    }
}
