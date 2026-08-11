using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesProductReturns.Validators
{
    public class IFisheriesProductReturnDtoValidator : AbstractValidator<IFisheriesProductReturnDto>
    {
        public IFisheriesProductReturnDtoValidator() 
        {
            //RuleFor(b => b.Nam)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
