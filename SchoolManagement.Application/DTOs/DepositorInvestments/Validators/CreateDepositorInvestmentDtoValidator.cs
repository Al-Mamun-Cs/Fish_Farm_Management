using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DepositorInvestments.Validators
{
    public class CreateDepositorInvestmentDtoValidator : AbstractValidator<CreateDepositorInvestmentDto>
    {
        public CreateDepositorInvestmentDtoValidator()  
        {
            Include(new IDepositorInvestmentDtoValidator()); 
        }
    }
}
