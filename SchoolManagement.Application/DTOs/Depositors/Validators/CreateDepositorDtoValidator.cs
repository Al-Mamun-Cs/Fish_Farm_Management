using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Depositors.Validators
{
    public class CreateDepositorDtoValidator : AbstractValidator<CreateDepositorDto>
    {
        public CreateDepositorDtoValidator()  
        {
            Include(new IDepositorDtoValidator()); 
        }
    }
}
