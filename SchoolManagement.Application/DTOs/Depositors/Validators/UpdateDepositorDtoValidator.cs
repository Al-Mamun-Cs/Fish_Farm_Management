using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Depositors.Validators
{
    public class UpdateDepositorDtoValidator : AbstractValidator<DepositorDto>
    {
        public UpdateDepositorDtoValidator()
        {
            Include(new IDepositorDtoValidator());

            RuleFor(b => b.DepositorId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

