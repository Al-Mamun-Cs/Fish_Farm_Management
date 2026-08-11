using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.FisheriesProductReturns.Validators
{
    public class UpdateFisheriesProductReturnDtoValidator : AbstractValidator<FisheriesProductReturnDto>
    {
        public UpdateFisheriesProductReturnDtoValidator()
        {
            Include(new IFisheriesProductReturnDtoValidator());

            RuleFor(b => b.FisheriesProductReturnId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

