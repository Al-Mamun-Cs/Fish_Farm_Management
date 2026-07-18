using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Ponds.Validators
{
    public class CreatePondDtoValidator : AbstractValidator<CreatePondDto>
    {
        public CreatePondDtoValidator()  
        {
            Include(new IPondDtoValidator()); 
        }
    }
}
