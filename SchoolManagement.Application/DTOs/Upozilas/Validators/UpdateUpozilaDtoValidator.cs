using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Upozilas.Validators
{
    public class UpdateUpozilaDtoValidator : AbstractValidator<UpozilaDto>
    {
        public UpdateUpozilaDtoValidator()
        {
            Include(new IUpozilaDtoValidator());

            RuleFor(b => b.UpazilaId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

