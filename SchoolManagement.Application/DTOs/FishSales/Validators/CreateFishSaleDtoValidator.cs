using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FishSales.Validators
{
    public class CreateFishSaleDtoValidator : AbstractValidator<CreateFishSaleDto>
    {
        public CreateFishSaleDtoValidator()  
        {
            Include(new IFishSaleDtoValidator()); 
        }
    }
}
