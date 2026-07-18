using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class Bulletin
    {
        public int BulletinId { get; set; }
        public string? BuletinDetails { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
