using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DatabaseBackups
{
    public interface IDatabaseBackupDto
    {
        public int DatabaseBackupId { get; set; }
        public string? FileName { get; set; }
        public DateTime? BackupDate { get; set; }
        public string? Status { get; set; }
        public int? Message { get; set; }
        public bool IsActive { get; set; }
    } 
}
