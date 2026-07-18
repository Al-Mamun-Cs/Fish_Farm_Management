using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesProductTypes.Validators
{
    public class UpdateFisheriesProductTypeDtoValidator : AbstractValidator<FisheriesProductTypeDto>
    {
        public UpdateFisheriesProductTypeDtoValidator()
        {
            Include(new IFisheriesProductTypeDtoValidator());

            RuleFor(b => b.FisheriesProductTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

