using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.ShopGoodSales.Validators
{
    public class UpdateShopGoodSaleDtoValidator : AbstractValidator<ShopGoodSaleDto>
    {
        public UpdateShopGoodSaleDtoValidator()
        {
            Include(new IShopGoodSaleDtoValidator());

            RuleFor(b => b.ShopGoodSaleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

