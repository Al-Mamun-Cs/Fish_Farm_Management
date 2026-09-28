using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public partial class ProjectType : BaseDomainEntity
    {
        public ProjectType()
        {
            Ponds = new HashSet<Pond>();
            ProjectSchedules = new HashSet<ProjectSchedule>();
            FisheriesInventorys = new HashSet<FisheriesInventory>();


        }

        public int ProjectTypeId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Pond> Ponds { get; set; }
        public virtual ICollection<ProjectSchedule> ProjectSchedules { get; set; }
        public virtual ICollection<FisheriesInventory> FisheriesInventorys { get; set; }
        
    }
}
