using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.CompanyInvestors.Validators
{
    public class UpdateCompanyInvestorDtoValidator : AbstractValidator<CompanyInvestorDto>
    {
        public UpdateCompanyInvestorDtoValidator()
        {
            Include(new ICompanyInvestorDtoValidator());

            RuleFor(b => b.CompanyInvestorId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

