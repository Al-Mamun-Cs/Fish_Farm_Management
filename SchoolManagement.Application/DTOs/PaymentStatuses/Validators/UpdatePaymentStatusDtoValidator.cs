using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.PaymentStatuses.Validators
{
    public class UpdatePaymentStatusDtoValidator : AbstractValidator<PaymentStatusDto>
    {
        public UpdatePaymentStatusDtoValidator()
        {
            Include(new IPaymentStatusDtoValidator());

            RuleFor(b => b.PaymentStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

