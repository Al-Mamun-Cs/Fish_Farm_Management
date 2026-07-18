using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.SupplierTypes.Validators
{
    public class CreateSupplierTypeDtoValidator : AbstractValidator<CreateSupplierTypeDto>
    {
        public CreateSupplierTypeDtoValidator()  
        {
            Include(new ISupplierTypeDtoValidator()); 
        }
    }
}
