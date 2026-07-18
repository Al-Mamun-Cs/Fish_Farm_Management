using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Suppliers.Validators
{
    public class UpdateSupplierDtoValidator : AbstractValidator<SupplierDto>
    {
        public UpdateSupplierDtoValidator()
        {
            Include(new ISupplierDtoValidator());

            RuleFor(b => b.SupplierId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

