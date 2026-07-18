using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Upozilas.Validators
{
    public class CreateUpozilaDtoValidator : AbstractValidator<CreateUpozilaDto>
    {
        public CreateUpozilaDtoValidator()  
        {
            Include(new IUpozilaDtoValidator()); 
        }
    }
}
