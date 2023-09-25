using AutoMapper;
using Azure;
using DataAccess.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using QuoteCalculator.API.Models.DTOs;
using Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace QuoteCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductFeeService _productFeeService;
        private readonly ICommonService _commonService;
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public QuoteController(IProductService productService, IMapper mapper, IProductFeeService productFeeService, ICommonService commonService, ILoanApplicationService loanApplicationService, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _productFeeService = productFeeService;
            _commonService = commonService;
            _loanApplicationService = loanApplicationService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult GetQuoteUrl([FromBody] LoanApplicationAPIRequestDto request)
        {

            List<string> validTitles = new List<string>() { "Mr." , "Mrs.", "Ms." };
            if (!validTitles.Contains(request.Title))
            {
                ModelState.AddModelError(nameof(request.Title), "Please provide a valid `title` ( Mr., Mrs., Ms.).");
            }

            if (!decimal.TryParse(request.AmountRequired, out _))
            {
                ModelState.AddModelError(nameof(request.AmountRequired), "Monetary(decimal) values are only accepted in `AmountRequired`.");
            }

            if (!int.TryParse(request.Term, out _))
            {
                ModelState.AddModelError(nameof(request.Term), "Numeric values are only accepted in `Term`.");
            }

            if(!new EmailAddressAttribute().IsValid(request.Email))
            {
                ModelState.AddModelError(nameof(request.Email), $"`{request.Email}` is an invalid email address.");
            }

            if (!DateTime.TryParseExact(request.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                ModelState.AddModelError(nameof(request.DateOfBirth), "Date format must be yyyy-MM-dd.");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            string baseUrl = _configuration["UIBaseURL"] + "quote/";

            var loanApplication = _loanApplicationService.GetLoanApplicationByBasicDetails(request.FirstName, request.LastName, Convert.ToDateTime(request.DateOfBirth));
            if(loanApplication != null)
            {
                return Ok(baseUrl + loanApplication.Id.ToString());
            }

            // Create
            LoanApplication newLoan = new LoanApplication()
            {
                AmountRequired = Convert.ToDecimal(request.AmountRequired),
                Term = Convert.ToInt32(request.Term),
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = Convert.ToDateTime(request.DateOfBirth),
                Mobile = request.Mobile,
                Email = request.Email,
                LoanApplicationStatus = 2
            };

            var results = _loanApplicationService.Add(newLoan);

            return Ok(baseUrl + results.Id.ToString());
        }

        [HttpGet("loanapplications/{id}")]
        public IActionResult GetLoanApplicationById(int id)
        {
            var loanApplication = _loanApplicationService.GetLoanApplicationById(id);
            if(loanApplication == null)
                return BadRequest("Loan Application Not Found");

            var mapped = _mapper.Map<LoanApplicationDto>(loanApplication);
            return Ok(mapped);
        }


        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = _productService.GetActiveProducts();
            var mapped = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(mapped);
        }

        [HttpPost("calculate/{productId}")]
        public IActionResult CalculateQuoteSummary(int productId, [FromBody] QuoteRequestDto request)
        {

            QuoteDetails details = _loanApplicationService.CalculateQuoteDetails(productId, request.Term, request.AmountRequired);
            var product = _productService.GetProductById(productId);
            var productFees = _productFeeService.GetProductFeesByProductId(productId);

            QuoteResponseDto response = new QuoteResponseDto();
            response.Product = _mapper.Map<ProductDto>(product);
            response.Fees = _mapper.Map<IEnumerable<ProductFeeDto>>(productFees);
            response.AmountRequired = request.AmountRequired;
            response.Term = request.Term;
            response.Repayment = details.Repayment;
            response.TotalInterest = details.TotalInterest;
            response.TotalFees = details.TotalFees;

            return Ok(response);
        }

        [HttpPost("loanapplications")]
        public IActionResult Apply([FromBody] LoanApplicationRequestDto request)
        {
            if(_commonService.ValidateAge(request.DateOfBirth))
            {
                ModelState.AddModelError(nameof(request.DateOfBirth), $"Age Below 18 is not allowed for the loan application.");
            }

            if (_loanApplicationService.ValidateMobile(request.Mobile))
            {
                ModelState.AddModelError(nameof(request.Mobile), $"Mobile Number `{request.Mobile}` is blacklisted!");
            }

            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                ModelState.AddModelError(nameof(request.Email), $"`{request.Email}` is an invalid email address.");
            }

            else
            {
                if (_loanApplicationService.ValidateDomain(request.Email))
                {
                    ModelState.AddModelError(nameof(request.Email), $"Domain from `{request.Email}` is blacklisted!");
                }
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var loanApplication = _mapper.Map<LoanApplication>(request);
            QuoteDetails details = _loanApplicationService.CalculateQuoteDetails(request.ProductId, request.Term, request.AmountRequired);
            loanApplication.LoanApplicationStatus = 3;
            loanApplication.Repayment = details.Repayment;
            loanApplication.TotalInterest = details.TotalInterest;
            loanApplication.TotalFees = details.TotalFees;

            if (request.Id != null)
            {
                // Update
                var updated = _loanApplicationService.Update(loanApplication);

                return Ok(_mapper.Map<LoanApplicationDto>(updated));
            }

            else
            {
                var add = _loanApplicationService.Add(loanApplication);
                return Ok(_mapper.Map<LoanApplicationDto>(add));
            }

            return Ok();
        }
    }
}
