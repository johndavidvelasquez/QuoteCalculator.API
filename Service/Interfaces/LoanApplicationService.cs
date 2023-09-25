using Azure.Core;
using DataAccess.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;
        private readonly IProductService _productService;
        private readonly IProductFeeService _productFeeService;

        public LoanApplicationService(IUnitOfWork unitOfWork, ICommonService commonService, IProductService productService, IProductFeeService productFeeService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
            _productService = productService;
            _productFeeService = productFeeService;
        }
        public LoanApplication GetLoanApplicationByBasicDetails(string firstName, string lastName, DateTime dateOfBirth)
        {
            var loanApplication = _unitOfWork.LoanApplications.GetAll().FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName && x.DateOfBirth == dateOfBirth && x.IsArchived == false);

            return loanApplication;
        }

        public LoanApplication GetLoanApplicationById(int id)
        {
            var loanApplication = _unitOfWork.LoanApplications.GetAll().FirstOrDefault(x => x.Id == id && x.IsArchived == false);

            return loanApplication;
        }

        public LoanApplication Add(LoanApplication application)
        {
            _unitOfWork.LoanApplications.Add(application);
            _unitOfWork.Complete();

            return application;
        }

        public LoanApplication Update(LoanApplication application)
        {
            var res = _unitOfWork.LoanApplications.GetById(application.Id);
            if(res != null)
            {
                res.AmountRequired = application.AmountRequired;
                res.Term = application.Term;
                res.Title = application.Title;
                res.FirstName = application.FirstName;
                res.LastName = application.LastName;
                res.DateOfBirth = application.DateOfBirth;
                res.Email = application.Email;
                res.Mobile = application.Mobile;
                res.LoanApplicationStatus = application.LoanApplicationStatus;
                res.ProductId = application.ProductId;
                res.Repayment = application.Repayment;
                res.TotalFees = application.TotalFees;
                res.TotalInterest = application.TotalInterest;
            }

            _unitOfWork.Complete();

            return res;
        }

        public QuoteDetails CalculateQuoteDetails(int productId, int term, decimal amountRequired)
        {
            QuoteDetails res = new QuoteDetails();

            var product = _productService.GetProductById(productId);
            var productFees = _productFeeService.GetProductFeesByProductId(productId);

            decimal totalFees = productFees.Sum(x => x.Amount);
            decimal repaymentWithoutFees = _commonService.CalculatePMT(product.InterestRate, term, amountRequired);
            decimal totalInterest = product.InterestRate == 0 ? 0 : (repaymentWithoutFees * term) - amountRequired;
            decimal feesToAddInRepayment = _commonService.CalculatePMT(0, term, totalFees);

            res.Repayment = repaymentWithoutFees + feesToAddInRepayment;
            res.TotalInterest = totalInterest;
            res.TotalFees = totalFees;

            return res;
        }

        public bool ValidateMobile(string mobile)
        {
            return _unitOfWork.BlackList.GetAll().Any(x => x.BlackListTypeId == 1 && x.Value == mobile);
        }

        public bool ValidateDomain(string email)
        {
            MailAddress address = new MailAddress(email);
            string host = address.Host;
            return _unitOfWork.BlackList.GetAll().Any(x => x.BlackListTypeId == 2 && x.Value == host);
        }
    }
}
