using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesInventoryOuts.Validators
{
    public class UpdateFisheriesInventoryOutDtoValidator : AbstractValidator<FisheriesInventoryOutDto>
    {
        public UpdateFisheriesInventoryOutDtoValidator()
        {
            Include(new IFisheriesInventoryOutDtoValidator());

            RuleFor(b => b.FisheriesInventoryOutId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

