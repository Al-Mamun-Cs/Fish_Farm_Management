using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.BloodGroups.Validators
{
    public class CreateBloodGroupDtoValidator : AbstractValidator<CreateBloodGroupDto>
    {
        public CreateBloodGroupDtoValidator()  
        {
            Include(new IBloodGroupDtoValidator()); 
        }
    }
}
