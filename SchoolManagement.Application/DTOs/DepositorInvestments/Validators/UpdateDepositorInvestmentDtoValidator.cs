using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.DepositorInvestments.Validators
{
    public class UpdateDepositorInvestmentDtoValidator : AbstractValidator<DepositorInvestmentDto>
    {
        public UpdateDepositorInvestmentDtoValidator()
        {
            Include(new IDepositorInvestmentDtoValidator());

            RuleFor(b => b.DepositorInvestmentId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

