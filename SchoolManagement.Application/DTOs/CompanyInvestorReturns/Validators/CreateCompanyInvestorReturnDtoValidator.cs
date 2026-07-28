using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CompanyInvestorReturns.Validators
{
    public class CreateCompanyInvestorReturnDtoValidator : AbstractValidator<CreateCompanyInvestorReturnDto>
    {
        public CreateCompanyInvestorReturnDtoValidator()  
        {
            Include(new ICompanyInvestorReturnDtoValidator()); 
        }
    }
}
