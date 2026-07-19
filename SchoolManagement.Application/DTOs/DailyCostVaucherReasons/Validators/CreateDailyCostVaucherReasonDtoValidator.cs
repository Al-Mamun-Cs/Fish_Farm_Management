using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DailyCostVaucherReasons.Validators
{
    public class CreateDailyCostVaucherReasonDtoValidator : AbstractValidator<CreateDailyCostVaucherReasonDto>
    {
        public CreateDailyCostVaucherReasonDtoValidator()  
        {
            Include(new IDailyCostVaucherReasonDtoValidator()); 
        }
    }
}
