namespace QuoteCalculator.API.Models.DTOs
{
    public class QuoteResponseDto
    {
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public decimal? TotalFees { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? Repayment { get; set; }
        public ProductDto Product { get; set; }
        public IEnumerable<ProductFeeDto> Fees { get; set; }

    }
}
