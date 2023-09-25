using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.API.Models.DTOs
{
    public class LoanApplicationRequestDto
    {
        [Required]
        public decimal AmountRequired { get; set; }
        [Required]
        public int Term { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }

        public int? Id { get; set; }

        public int ProductId { get; set; }

    }
}
