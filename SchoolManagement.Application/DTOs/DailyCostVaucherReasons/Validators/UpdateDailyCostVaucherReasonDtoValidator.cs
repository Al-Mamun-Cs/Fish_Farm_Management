using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.DailyCostVaucherReasons.Validators
{
    public class UpdateDailyCostVaucherReasonDtoValidator : AbstractValidator<DailyCostVaucherReasonDto>
    {
        public UpdateDailyCostVaucherReasonDtoValidator()
        {
            Include(new IDailyCostVaucherReasonDtoValidator());

            RuleFor(b => b.DailyCostVaucherReasonId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

