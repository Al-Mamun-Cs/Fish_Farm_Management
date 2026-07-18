using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Religions.Validators
{
    public class UpdateReligionDtoValidator : AbstractValidator<ReligionDto>
    {
        public UpdateReligionDtoValidator()
        {
            Include(new IReligionDtoValidator());

            RuleFor(b => b.ReligionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

