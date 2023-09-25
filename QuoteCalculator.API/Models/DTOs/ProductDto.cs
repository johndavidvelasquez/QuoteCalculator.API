namespace QuoteCalculator.API.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal InterestRate { get; set; }
        public int MinMonth { get; set; }
        public int MaxMonth { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}
