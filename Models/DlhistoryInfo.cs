using System;
namespace DLHAPI.Models
{
	public class DlhistoryInfo
	{
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; }
        public string LicenseClass { get; set; }
    }
}

