using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CompanyInvestors.Validators
{
    public class CreateCompanyInvestorDtoValidator : AbstractValidator<CreateCompanyInvestorDto>
    {
        public CreateCompanyInvestorDtoValidator()  
        {
            Include(new ICompanyInvestorDtoValidator()); 
        }
    }
}
