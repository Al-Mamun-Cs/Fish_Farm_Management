using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesInventorys.Validators
{
    public class CreateFisheriesInventoryDtoValidator : AbstractValidator<CreateFisheriesInventoryDto>
    {
        public CreateFisheriesInventoryDtoValidator()  
        {
            Include(new IFisheriesInventoryDtoValidator()); 
        }
    }
}
