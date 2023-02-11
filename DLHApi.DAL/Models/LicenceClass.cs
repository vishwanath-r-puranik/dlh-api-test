namespace DLHApi.DAL.Models;

public partial class LicenceClass
{
    public int LicClassId { get; set; }

    public string? LicClass { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<DlhistoryInfo> DlhistoryInfos { get; } = new List<DlhistoryInfo>();

    public virtual ICollection<LicenceDetail> LicenceDetails { get; } = new List<LicenceDetail>();
}
