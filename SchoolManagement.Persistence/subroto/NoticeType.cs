using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class NoticeType
    {
        public NoticeType()
        {
            NoticeInfos = new HashSet<NoticeInfo>();
        }

        public int NoticeTypeId { get; set; }
        public string? Name { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<NoticeInfo> NoticeInfos { get; set; }
    }
}
