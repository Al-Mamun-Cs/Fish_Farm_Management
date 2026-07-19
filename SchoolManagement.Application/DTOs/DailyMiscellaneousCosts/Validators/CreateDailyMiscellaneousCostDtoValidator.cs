using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DailyMiscellaneousCosts.Validators
{
    public class CreateDailyMiscellaneousCostDtoValidator : AbstractValidator<CreateDailyMiscellaneousCostDto>
    {
        public CreateDailyMiscellaneousCostDtoValidator()  
        {
            Include(new IDailyMiscellaneousCostDtoValidator()); 
        }
    }
}
