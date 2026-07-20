using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopGoodSales.Validators
{
    public class CreateShopGoodSaleDtoValidator : AbstractValidator<CreateShopGoodSaleDto>
    {
        public CreateShopGoodSaleDtoValidator()  
        {
            Include(new IShopGoodSaleDtoValidator()); 
        }
    }
}
