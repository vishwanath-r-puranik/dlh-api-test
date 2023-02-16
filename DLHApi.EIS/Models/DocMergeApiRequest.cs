using DLHApi.EIS.Models;

namespace DLHApi.EIS.Models
{
    public class DocMergeApiRequest
	{
        public int? Id { get; set; }
        public string? MVID { get; set; }
        public string? FullName { get; set; }
        public string? Dob { get; set; }
        public string? Address { get; set; }
        public string? LicenseClass { get; set; }
        public string? ServiceType { get; set; }
        public string? DateOfIssue { get; set; }
        public string? DateOfExpire { get; set; }
        public string? LicenseNumber { get; set; }
        public string? GDl { get; set; }
        public string? GDlExitDate { get; set; }
        public string? Conditions { get; set; }
        public IList<DlhistoryDisplayInfo?>? historyInfo { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}
