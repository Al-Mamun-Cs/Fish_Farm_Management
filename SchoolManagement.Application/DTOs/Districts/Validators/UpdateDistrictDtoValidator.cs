using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Districts.Validators
{
    public class UpdateDistrictDtoValidator : AbstractValidator<DistrictDto>
    {
        public UpdateDistrictDtoValidator()
        {
            Include(new IDistrictDtoValidator());

            RuleFor(b => b.DistrictId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

