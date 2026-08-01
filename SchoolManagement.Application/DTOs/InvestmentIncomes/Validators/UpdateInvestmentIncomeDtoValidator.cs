using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.InvestmentIncomes.Validators
{
    public class UpdateInvestmentIncomeDtoValidator : AbstractValidator<InvestmentIncomeDto>
    {
        public UpdateInvestmentIncomeDtoValidator()
        {
            Include(new IInvestmentIncomeDtoValidator());

            RuleFor(b => b.InvestmentIncomeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

