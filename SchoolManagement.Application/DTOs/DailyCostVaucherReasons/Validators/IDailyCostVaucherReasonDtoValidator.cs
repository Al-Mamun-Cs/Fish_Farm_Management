using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.DailyCostVaucherReasons.Validators
{
    public class IDailyCostVaucherReasonDtoValidator : AbstractValidator<IDailyCostVaucherReasonDto>
    {
        public IDailyCostVaucherReasonDtoValidator() 
        {
            //RuleFor(b => b.Nam)
            //    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
