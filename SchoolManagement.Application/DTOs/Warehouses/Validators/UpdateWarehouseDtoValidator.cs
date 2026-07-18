using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Warehouses.Validators
{
    public class UpdateWarehouseDtoValidator : AbstractValidator<WarehouseDto>
    {
        public UpdateWarehouseDtoValidator()
        {
            Include(new IWarehouseDtoValidator());

            RuleFor(b => b.WarehouseId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

