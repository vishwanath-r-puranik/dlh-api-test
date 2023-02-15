using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApi.Shared
{
    public class UpdateAuditRequest
    {
        public string? RequestId { get; set; }
        public DateTime? PaymentDateTimeStamp { get; set; }
        public DateTime? DataRetrievedDateTimeStamp { get; set; }
        public DateTime? ReportGeneratedDateTimeStamp { get; set; }
        public string? Mvid { get; set; }
        public string? RecordStatus { get; set; }
        public string? MOVESTxServiceNo { get; set; }
        public string? ROADSUserID { get; set; }
        public string? MOVESSessionID { get; set; }
    }
}
