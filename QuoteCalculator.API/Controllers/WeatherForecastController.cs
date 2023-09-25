using DataAccess.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace QuoteCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork, IProductService productService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            List<Product> products = new List<Product>();
            //products.Add(new Product { Name = "ProductA", Description = "0% Interest Rate promo!", MinMonth = 2, MaxMonth = 24, MinAmount = 500, MaxAmount = 5000, InterestRate = 0, MonthInterestDiscount = 0});
            //products.Add(new Product { Name = "ProductB", Description = "Interest Rate as low as 8% APR! What's more is the interest rate for the first two months is on us!", MinMonth = 6, MaxMonth = 36, MinAmount = 2500, MaxAmount = 20000, InterestRate = 8, MonthInterestDiscount = 2 });
            //products.Add(new Product { Name = "ProductC", Description = "7.5% APR. Up to 5 years repayment!", MinMonth = 2, MaxMonth = 60, MinAmount = 500, MaxAmount = 5000, InterestRate = (decimal)7.5, MonthInterestDiscount = 0 });
            products.Add(new Product { Name = "ProductD", Description = "8% APR + No fees!", MinMonth = 2, MaxMonth = 60, MinAmount = 2000, MaxAmount = 10000, InterestRate = 8, MonthInterestDiscount = 0 });


            List<ProductFee> pfs = new List<ProductFee>();


            pfs.Add(new ProductFee() { Name = "Establishment Fee", Amount = 100, ProductId = 1, Description = "" });
            pfs.Add(new ProductFee() { Name = "Processing Fee", Amount = 50, ProductId = 1, Description = "" });
            pfs.Add(new ProductFee() { Name = "Establishment Fee", Amount = 50, ProductId = 2, Description = "" });
            pfs.Add(new ProductFee() { Name = "Establishment Fee", Amount = 50, ProductId = 3, Description = "" });
            pfs.Add(new ProductFee() { Name = "Establishment Fee", Amount = 0, ProductId = 4, Description = "" });


            //var product = _unitOfWork.Products.GetById(1);
            //product.ProductFees.Add(new ProductFee() { Name = "Establishment Fee", Amount = 100, Description = "" });
            //product.ProductFees.Add(new ProductFee() { Name = "Processing Fee", Amount = 50, Description = "" });


            _unitOfWork.Products.AddRange(products);
            //_unitOfWork.ProductFees.AddRange(pfs);
            _unitOfWork.Complete();


            var test = _productService.GetActiveProducts();


            return Ok(test);

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}