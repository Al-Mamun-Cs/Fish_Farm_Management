using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.ShopHandCashWithdrows.Validators
{
    public class UpdateShopHandCashWithdrowDtoValidator : AbstractValidator<ShopHandCashWithdrowDto>
    {
        public UpdateShopHandCashWithdrowDtoValidator()
        {
            Include(new IShopHandCashWithdrowDtoValidator());

            RuleFor(b => b.ShopHandCashWithdrowId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

