using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagement.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }       
        public string PhotoPath { get; set; }       
        public string SignaturePath { get; set; }       
        public string PNo { get; set; }
        public string BranchId { get; set; }
        public int SupplierId { get; set; }
        public int DepartmentPostPositionId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string InActiveBy { get; set; }
        public DateTime? InActiveDate { get; set; }
    }
}
