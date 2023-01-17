using System;
namespace DLHAPI.Models
{
	public class DlhistoryModel
	{
        public int Id { get; set; }
        public string? MVID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly Dob { get; set; }
        public string? Address { get; set; }
        public string? LicenseClass { get; set; }
        public string? ServiceType { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpire { get; set; }
        public string? LicenseNumber { get; set; }
        public Boolean GDl { get; set; }
        public DateTime? GDlExitDate { get; set; }
        public string? TransactionId { get; set; }
        public IList<DlhistoryInfo>? historyInfo { get; set; }
    }
}

