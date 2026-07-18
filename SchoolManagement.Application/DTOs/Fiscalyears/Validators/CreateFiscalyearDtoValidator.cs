using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Fiscalyears.Validators
{
    public class CreateFiscalyearDtoValidator : AbstractValidator<CreateFiscalyearDto>
    {
        public CreateFiscalyearDtoValidator()  
        {
            Include(new IFiscalyearDtoValidator()); 
        }
    }
}
