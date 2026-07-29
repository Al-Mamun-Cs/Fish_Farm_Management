using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FishSales.Validators
{
    public class UpdateFishSaleDtoValidator : AbstractValidator<FishSaleDto>
    {
        public UpdateFishSaleDtoValidator()
        {
            Include(new IFishSaleDtoValidator());

            RuleFor(b => b.FishSaleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

