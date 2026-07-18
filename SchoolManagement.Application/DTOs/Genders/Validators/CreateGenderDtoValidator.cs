using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Genders.Validators
{
    public class CreateGenderDtoValidator : AbstractValidator<CreateGenderDto>
    {
        public CreateGenderDtoValidator()  
        {
            Include(new IGenderDtoValidator()); 
        }
    }
}
