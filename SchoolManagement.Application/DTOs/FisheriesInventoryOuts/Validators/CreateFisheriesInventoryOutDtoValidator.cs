using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesInventoryOuts.Validators
{
    public class CreateFisheriesInventoryOutDtoValidator : AbstractValidator<CreateFisheriesInventoryOutDto>
    {
        public CreateFisheriesInventoryOutDtoValidator()  
        {
            Include(new IFisheriesInventoryOutDtoValidator()); 
        }
    }
}
