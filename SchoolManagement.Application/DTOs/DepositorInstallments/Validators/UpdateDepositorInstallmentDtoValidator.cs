using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.DepositorInstallments.Validators
{
    public class UpdateDepositorInstallmentDtoValidator : AbstractValidator<DepositorInstallmentDto>
    {
        public UpdateDepositorInstallmentDtoValidator()
        {
            Include(new IDepositorInstallmentDtoValidator());

            RuleFor(b => b.DepositorInstallmentId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

