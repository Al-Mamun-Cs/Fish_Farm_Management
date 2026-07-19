using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.ShopInventorys.Validators
{
    public class UpdateShopInventoryDtoValidator : AbstractValidator<ShopInventoryDto>
    {
        public UpdateShopInventoryDtoValidator()
        {
            Include(new IShopInventoryDtoValidator());

            RuleFor(b => b.ShopInventoryId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

