using System;
using System.Collections.Generic;

namespace DLHApi.DAL.Models;

public partial class Licence
{
    public long LicenceId { get; set; }

    public decimal Mvid { get; set; }

    public string? LicNumber { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();

    public virtual ICollection<DlhistoryInfo> DlhistoryInfos { get; } = new List<DlhistoryInfo>();

    public virtual ICollection<LicenceDetail> LicenceDetails { get; } = new List<LicenceDetail>();
}
