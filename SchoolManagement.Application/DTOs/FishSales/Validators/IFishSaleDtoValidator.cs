using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FishSales.Validators
{
    public class IFishSaleDtoValidator : AbstractValidator<IFishSaleDto>
    {
        public IFishSaleDtoValidator() 
        {
            //RuleFor(b => b.Nam)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
