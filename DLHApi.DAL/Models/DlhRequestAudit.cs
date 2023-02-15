namespace DLHApi.DAL.Models;

public partial class DlhRequestAudit
{

    public string? RequestId { get; set; }
    public DateTime? RequestDateTimeStamp { get; set; }
    public DateTime? PaymentDateTimeStamp { get; set; }
    public string? Mvid { get; set; }
    public string? RecordStatus { get; set; }
    public string? MOVESTxServiceNo { get; set; }
    public string? ROADSUserID { get; set; }
    public string? MOVESSessionID { get; set; }

}
