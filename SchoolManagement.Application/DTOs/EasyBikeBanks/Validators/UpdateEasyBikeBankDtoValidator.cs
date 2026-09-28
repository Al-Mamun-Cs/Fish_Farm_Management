using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.EasyBikeBanks.Validators
{
    public class UpdateEasyBikeBankDtoValidator : AbstractValidator<EasyBikeBankDto>
    {
        public UpdateEasyBikeBankDtoValidator()
        {
            Include(new IEasyBikeBankDtoValidator());

            RuleFor(b => b.EasyBikeBankId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

