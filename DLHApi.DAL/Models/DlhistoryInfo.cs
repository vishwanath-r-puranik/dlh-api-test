namespace DLHApi.DAL.Models
{
    public partial class DlhistoryInfo
    {
        public int DlhisInfoId { get; set; }

        public int? Mvid { get; set; }

        public string? ServiceType { get; set; }

        public DateTime? IssueDate { get; set; }

        public int? LicClassId { get; set; }

        public virtual LicenceClass? LicClass { get; set; }

        public virtual Licence? Mv { get; set; }
    }
}
