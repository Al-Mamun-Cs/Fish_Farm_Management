using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ProjectTypes.Validators
{
    public class CreateProjectTypeDtoValidator : AbstractValidator<CreateProjectTypeDto>
    {
        public CreateProjectTypeDtoValidator()  
        {
            Include(new IProjectTypeDtoValidator()); 
        }
    }
}
