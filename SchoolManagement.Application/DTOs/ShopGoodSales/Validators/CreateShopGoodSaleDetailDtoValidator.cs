using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopGoodSales.Validators
{
    public class CreateShopGoodSaleDetailDtoValidator : AbstractValidator<CreateShopGoodSaleDetailDto>
    {
        public CreateShopGoodSaleDetailDtoValidator()  
        {
            Include(new IShopGoodSaleDtoValidator()); 
        }
    }
}
