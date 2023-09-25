using DataAccess.Interfaces;
using Moq;
using QuoteCalculator.API.Controllers;
using Service.Interfaces;

namespace Tests
{
    public class QuoteCalculatorTests
    {

        //public Mock<ICommonService> mock = new Mock<ICommonService>();

        public ICommonService _commonService = new CommonService();

        [Theory]
        // Note: Data compared with PMT Excel Results. For interest rate divided the interest into 100 and 12 ( / 100 / 12 ) to get the results
        [InlineData(8, 24, 5000, 226.14)]
        [InlineData(7.5, 24, 5000, 225)] // Test decimal interest
        [InlineData(8, 24, 5500.50, 248.77)] // Test decimal amount
        [InlineData(7.5, 24, 5500.50, 247.52)] // Test decimal interest and amount
        [InlineData(0, 24, 5000, 208.33)] // 0 interest

        public void TestCalculatePMT1(decimal interestRate, int payments, decimal principal, decimal expectedAnswer)
        {
            decimal results = _commonService.CalculatePMT(interestRate, payments, principal);

            Assert.Equal(expectedAnswer, results);

        }
    }
}