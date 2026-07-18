using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Divisions.Validators
{
    public class CreateDivisionDtoValidator : AbstractValidator<CreateDivisionDto>
    {
        public CreateDivisionDtoValidator()  
        {
            Include(new IDivisionDtoValidator()); 
        }
    }
}
