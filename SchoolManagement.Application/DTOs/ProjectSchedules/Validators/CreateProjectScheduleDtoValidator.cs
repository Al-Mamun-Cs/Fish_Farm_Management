using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ProjectSchedules.Validators
{
    public class CreateProjectScheduleDtoValidator : AbstractValidator<CreateProjectScheduleDto>
    {
        public CreateProjectScheduleDtoValidator()  
        {
            Include(new IProjectScheduleDtoValidator()); 
        }
    }
}
