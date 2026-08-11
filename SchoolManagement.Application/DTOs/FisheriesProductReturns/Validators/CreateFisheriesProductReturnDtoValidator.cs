using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesProductReturns.Validators
{
    public class CreateFisheriesProductReturnDtoValidator : AbstractValidator<CreateFisheriesProductReturnDto>
    {
        public CreateFisheriesProductReturnDtoValidator()  
        {
            Include(new IFisheriesProductReturnDtoValidator()); 
        }
    }
}
