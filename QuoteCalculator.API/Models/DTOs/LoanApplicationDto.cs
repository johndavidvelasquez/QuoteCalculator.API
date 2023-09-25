using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.API.Models.DTOs
{
    public class LoanApplicationDto
    {
        public int Id { get; set; }

        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int? LoanApplicationStatus { get; set; }

        public int? ProductId { get; set; }
        public decimal? TotalFees { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? Repayment { get; set; }
    }
}



