using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Divisions.Validators
{
    public class UpdateDivisionDtoValidator : AbstractValidator<DivisionDto>
    {
        public UpdateDivisionDtoValidator()
        {
            Include(new IDivisionDtoValidator());

            RuleFor(b => b.DivisionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

