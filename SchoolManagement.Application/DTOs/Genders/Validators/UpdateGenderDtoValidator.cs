using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Genders.Validators
{
    public class UpdateGenderDtoValidator : AbstractValidator<GenderDto>
    {
        public UpdateGenderDtoValidator()
        {
            Include(new IGenderDtoValidator());

            RuleFor(b => b.GenderId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

