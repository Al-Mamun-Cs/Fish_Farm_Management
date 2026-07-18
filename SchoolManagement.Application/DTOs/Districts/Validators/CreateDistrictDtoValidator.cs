using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Districts.Validators
{
    public class CreateDistrictDtoValidator : AbstractValidator<CreateDistrictDto>
    {
        public CreateDistrictDtoValidator()  
        {
            Include(new IDistrictDtoValidator()); 
        }
    }
}
