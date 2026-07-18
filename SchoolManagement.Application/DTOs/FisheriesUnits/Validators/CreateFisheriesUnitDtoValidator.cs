using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesUnits.Validators
{
    public class CreateFisheriesUnitDtoValidator : AbstractValidator<CreateFisheriesUnitDto>
    {
        public CreateFisheriesUnitDtoValidator()  
        {
            Include(new IFisheriesUnitDtoValidator()); 
        }
    }
}
