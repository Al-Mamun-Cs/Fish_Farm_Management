using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.DailyMiscellaneousCosts.Validators
{
    public class UpdateDailyMiscellaneousCostDtoValidator : AbstractValidator<DailyMiscellaneousCostDto>
    {
        public UpdateDailyMiscellaneousCostDtoValidator()
        {
            Include(new IDailyMiscellaneousCostDtoValidator());

            RuleFor(b => b.DailyMiscellaneousCostId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

