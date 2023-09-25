using AutoMapper;
using Domain.Entities;
using QuoteCalculator.API.Models.DTOs;

namespace QuoteCalculator.API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductFee, ProductFeeDto>();
            CreateMap<ProductFeeDto, ProductFee>();
            CreateMap<LoanApplication, LoanApplicationDto>();
            CreateMap<LoanApplicationDto, LoanApplication>();
            CreateMap<LoanApplication, LoanApplicationAPIRequestDto>();
            CreateMap<LoanApplicationAPIRequestDto, LoanApplication>();
            CreateMap<LoanApplication, LoanApplicationRequestDto>();
            CreateMap<LoanApplicationRequestDto, LoanApplication>();

        }
    }
}
