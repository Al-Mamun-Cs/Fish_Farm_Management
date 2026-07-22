using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopHandCashWithdrows.Validators
{
    public class CreateShopHandCashWithdrowDtoValidator : AbstractValidator<CreateShopHandCashWithdrowDto>
    {
        public CreateShopHandCashWithdrowDtoValidator()  
        {
            Include(new IShopHandCashWithdrowDtoValidator()); 
        }
    }
}
