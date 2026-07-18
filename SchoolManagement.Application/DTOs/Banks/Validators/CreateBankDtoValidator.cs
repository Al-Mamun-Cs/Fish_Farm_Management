using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Banks.Validators
{
    public class CreateBankDtoValidator : AbstractValidator<CreateBankDto>
    {
        public CreateBankDtoValidator()  
        {
            Include(new IBankDtoValidator()); 
        }
    }
}
