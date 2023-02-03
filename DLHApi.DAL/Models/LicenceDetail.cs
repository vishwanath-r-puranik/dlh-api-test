using System;
using System.Collections.Generic;

namespace DLHApi.DAL.Models;

public partial class LicenceDetail
{
    public long LicDetailId { get; set; }

    public decimal? Mvid { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? ServiceType { get; set; }

    public int? LicClassId { get; set; }

    public string? Gdlstatus { get; set; }

    public DateTime? GdlexitDate { get; set; }

    public int LicCond { get; set; }

    public virtual LicenceClass? LicClass { get; set; }

    public virtual ICollection<LicenceCondition> LicenceConditions { get; } = new List<LicenceCondition>();

    public virtual Licence? Mv { get; set; }
}
