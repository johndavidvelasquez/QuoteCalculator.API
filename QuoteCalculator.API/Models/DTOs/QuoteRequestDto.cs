namespace QuoteCalculator.API.Models.DTOs
{
    public class QuoteRequestDto
    {
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }

    }
}
