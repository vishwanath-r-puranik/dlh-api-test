using DLHApi.DAL.Models;

namespace DLHApi.DTO.V1.DTO
{
    public class DlhistoryModel
    {
        public string? MVID { get; set; }
        public string? FullName { get; set; }
        //public string? MiddleName { get; set; }
        //public string? LastName { get; set; }
        public string? Dob { get; set; }
      //  public string? Address { get; set; }
        public string? LicenseClass { get; set; }
        public string? ServiceType { get; set; }
        public string? DateOfIssue { get; set; }
        public string? DateOfExpire { get; set; }
        public string? LicenseNumber { get; set; }
        public string? GDl { get; set; }
        public string? GDlExitDate { get; set; }
        //  public string? TransactionId { get; set; }
        public string? Conditions { get; set; }
        public IList<DlhHistoryDisplay>? historyInfo { get; set; }
        //public byte[]? FileContent { get; set; }
        //public string? ContentType { get; set; }
        //public string? FileName { get; set; }
    }
}
