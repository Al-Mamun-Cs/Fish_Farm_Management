using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Countrys.Validators
{
    public class UpdateCountryDtoValidator : AbstractValidator<CountryDto>
    {
        public UpdateCountryDtoValidator()
        {
            Include(new ICountryDtoValidator());

            RuleFor(b => b.CountryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

