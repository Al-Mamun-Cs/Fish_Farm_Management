using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesUnits.Validators
{
    public class UpdateFisheriesUnitDtoValidator : AbstractValidator<FisheriesUnitDto>
    {
        public UpdateFisheriesUnitDtoValidator()
        {
            Include(new IFisheriesUnitDtoValidator());

            RuleFor(b => b.FisheriesUnitId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

