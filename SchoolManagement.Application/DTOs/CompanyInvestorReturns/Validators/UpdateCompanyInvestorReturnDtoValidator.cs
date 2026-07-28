using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.CompanyInvestorReturns.Validators
{
    public class UpdateCompanyInvestorReturnDtoValidator : AbstractValidator<CompanyInvestorReturnDto>
    {
        public UpdateCompanyInvestorReturnDtoValidator()
        {
            Include(new ICompanyInvestorReturnDtoValidator());

            RuleFor(b => b.CompanyInvestorReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

