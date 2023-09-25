using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.API.Models.DTOs
{
    public class LoanApplicationAPIRequestDto
    {
        [Required]
        public string AmountRequired { get; set; }
        [Required]
        public string Term { get; set; }
        [Required] 
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
