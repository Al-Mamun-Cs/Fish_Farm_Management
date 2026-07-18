using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.PaymentStatuses.Validators
{
    public class CreatePaymentStatusDtoValidator : AbstractValidator<CreatePaymentStatusDto>
    {
        public CreatePaymentStatusDtoValidator()  
        {
            Include(new IPaymentStatusDtoValidator()); 
        }
    }
}
