using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Fiscalyears.Validators
{
    public class UpdateFiscalyearDtoValidator : AbstractValidator<FiscalyearDto>
    {
        public UpdateFiscalyearDtoValidator()
        {
            Include(new IFiscalyearDtoValidator());

            RuleFor(b => b.FiscalyearId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

