using DataAccess.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public class CommonService : ICommonService
    {
        public decimal CalculatePMT(decimal interestRate, int payments, decimal principal)
        {
            if(interestRate > 0)
            {
                var rate = (double) interestRate / 100 / 12;
                var denominator = Math.Pow((1 + rate), payments) - 1;
                return Math.Round(new decimal((rate + (rate / denominator)) * (double)principal), 2);
            }

            return payments > 0 ? Math.Round(new decimal((double)principal / (double)payments), 2) : 0;
        }

        public bool ValidateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age))
                age--;

            return age >= 18 ? false : true;
        }
    }
}
