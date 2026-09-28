using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.EasyBikeBanks.Validators
{
    public class CreateEasyBikeBankDtoValidator : AbstractValidator<CreateEasyBikeBankDto>
    {
        public CreateEasyBikeBankDtoValidator()  
        {
            Include(new IEasyBikeBankDtoValidator()); 
        }
    }
}
