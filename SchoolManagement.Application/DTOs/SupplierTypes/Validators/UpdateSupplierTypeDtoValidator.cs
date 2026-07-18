using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.SupplierTypes.Validators
{
    public class UpdateSupplierTypeDtoValidator : AbstractValidator<SupplierTypeDto>
    {
        public UpdateSupplierTypeDtoValidator()
        {
            Include(new ISupplierTypeDtoValidator());

            RuleFor(b => b.SupplierTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

