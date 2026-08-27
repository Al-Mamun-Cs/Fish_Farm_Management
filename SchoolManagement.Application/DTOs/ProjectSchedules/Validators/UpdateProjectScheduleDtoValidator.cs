using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace SchoolManagement.Application.DTOs.ProjectSchedules.Validators
{
    public class UpdateProjectScheduleDtoValidator : AbstractValidator<ProjectScheduleDto>
    {
        public UpdateProjectScheduleDtoValidator()
        {
            Include(new IProjectScheduleDtoValidator());

            RuleFor(b => b.ProjectScheduleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

