using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesProductTypes.Validators
{
    public class CreateFisheriesProductTypeDtoValidator : AbstractValidator<CreateFisheriesProductTypeDto>
    {
        public CreateFisheriesProductTypeDtoValidator()  
        {
            Include(new IFisheriesProductTypeDtoValidator()); 
        }
    }
}
