using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class NoticeInfo
    {
        public int NoticeInfoId { get; set; }
        public int? NoticeTypeId { get; set; }
        public string? NoticeTitle { get; set; }
        public string? UploadPdffile { get; set; }
        public int? MemberInfoId { get; set; }
        public DateTime? NoticeEndDate { get; set; }
        public string? Remarks { get; set; }
        public int? ViewStatus { get; set; }
        public int? DetailViewStatus { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual NoticeType? NoticeType { get; set; }
    }
}
