using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.ProjectTypes.Validators
{
    public class UpdateProjectTypeDtoValidator : AbstractValidator<ProjectTypeDto>
    {
        public UpdateProjectTypeDtoValidator()
        {
            Include(new IProjectTypeDtoValidator());

            RuleFor(b => b.ProjectTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

