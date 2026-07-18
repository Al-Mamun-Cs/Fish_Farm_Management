using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Banks.Validators
{
    public class UpdateBankDtoValidator : AbstractValidator<BankDto>
    {
        public UpdateBankDtoValidator()
        {
            Include(new IBankDtoValidator());

            RuleFor(b => b.BankId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

