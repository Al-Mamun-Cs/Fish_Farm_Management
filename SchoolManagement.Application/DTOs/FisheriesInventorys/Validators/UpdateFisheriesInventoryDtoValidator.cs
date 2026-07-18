using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesInventorys.Validators
{
    public class UpdateFisheriesInventoryDtoValidator : AbstractValidator<FisheriesInventoryDto>
    {
        public UpdateFisheriesInventoryDtoValidator()
        {
            Include(new IFisheriesInventoryDtoValidator());

            RuleFor(b => b.FisheriesInventoryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

