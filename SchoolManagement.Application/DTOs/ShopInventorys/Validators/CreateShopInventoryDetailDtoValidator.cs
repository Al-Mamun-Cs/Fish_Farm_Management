using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopInventorys.Validators
{
    public class CreateShopInventoryDetailDtoValidator : AbstractValidator<CreateShopInventoryDetailDto>
    {
        public CreateShopInventoryDetailDtoValidator()  
        {
            Include(new IShopInventoryDtoValidator()); 
        }
    }
}
