using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DepositorInstallments.Validators
{
    public class CreateDepositorInstallmentDtoValidator : AbstractValidator<CreateDepositorInstallmentDto>
    {
        public CreateDepositorInstallmentDtoValidator()  
        {
            Include(new IDepositorInstallmentDtoValidator()); 
        }
    }
}
