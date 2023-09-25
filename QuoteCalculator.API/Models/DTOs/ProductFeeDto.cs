namespace QuoteCalculator.API.Models.DTOs
{
    public class ProductFeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
