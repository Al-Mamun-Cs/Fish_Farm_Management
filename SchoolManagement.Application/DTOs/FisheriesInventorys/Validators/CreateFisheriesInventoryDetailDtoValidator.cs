using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesInventorys.Validators
{
    public class CreateFisheriesInventoryDetailDtoValidator : AbstractValidator<CreateFisheriesInventoryDetailDto>
    {
        public CreateFisheriesInventoryDetailDtoValidator()  
        {
            Include(new IFisheriesInventoryDtoValidator()); 
        }
    }
}
