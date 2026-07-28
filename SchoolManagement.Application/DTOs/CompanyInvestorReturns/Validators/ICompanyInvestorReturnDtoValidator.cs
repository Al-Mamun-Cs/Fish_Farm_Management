using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.CompanyInvestorReturns.Validators
{
    public class ICompanyInvestorReturnDtoValidator : AbstractValidator<ICompanyInvestorReturnDto>
    {
        public ICompanyInvestorReturnDtoValidator() 
        {
            //RuleFor(b => b.Nam)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
