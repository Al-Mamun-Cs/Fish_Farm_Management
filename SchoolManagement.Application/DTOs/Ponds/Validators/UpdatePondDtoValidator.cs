using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Ponds.Validators
{
    public class UpdatePondDtoValidator : AbstractValidator<PondDto>
    {
        public UpdatePondDtoValidator()
        {
            Include(new IPondDtoValidator());

            RuleFor(b => b.PondId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

