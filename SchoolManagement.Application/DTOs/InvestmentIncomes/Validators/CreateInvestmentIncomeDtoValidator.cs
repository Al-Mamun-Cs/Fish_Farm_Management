using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.InvestmentIncomes.Validators
{
    public class CreateInvestmentIncomeDtoValidator : AbstractValidator<CreateInvestmentIncomeDto>
    {
        public CreateInvestmentIncomeDtoValidator()  
        {
            Include(new IInvestmentIncomeDtoValidator()); 
        }
    }
}
