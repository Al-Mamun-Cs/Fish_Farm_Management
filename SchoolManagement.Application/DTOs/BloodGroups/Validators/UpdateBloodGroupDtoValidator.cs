using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.BloodGroups.Validators
{
    public class UpdateBloodGroupDtoValidator : AbstractValidator<BloodGroupDto>
    {
        public UpdateBloodGroupDtoValidator()
        {
            Include(new IBloodGroupDtoValidator());

            RuleFor(b => b.BloodGroupId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

