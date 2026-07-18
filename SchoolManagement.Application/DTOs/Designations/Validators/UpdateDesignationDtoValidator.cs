using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.Designations.Validators
{
    public class UpdateDesignationDtoValidator : AbstractValidator<DesignationDto>
    {
        public UpdateDesignationDtoValidator()
        {
            Include(new IDesignationDtoValidator());

            RuleFor(b => b.DesignationId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

