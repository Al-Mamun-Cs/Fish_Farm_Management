using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopInventorys.Validators
{
    public class CreateShopInventoryDtoValidator : AbstractValidator<CreateShopInventoryDto>
    {
        public CreateShopInventoryDtoValidator()  
        {
            Include(new IShopInventoryDtoValidator()); 
        }
    }
}
