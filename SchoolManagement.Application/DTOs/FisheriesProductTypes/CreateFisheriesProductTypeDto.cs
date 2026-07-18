using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesProductTypes
{
    public class CreateFisheriesProductTypeDto : IFisheriesProductTypeDto
    {
        public int FisheriesProductTypeId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public bool IsActive { get; set; }
    }
}
