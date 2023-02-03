using System;
using System.Collections.Generic;

namespace DLHApi.DAL.Models;

public partial class LicenceCondition
{
    public int LicCondId { get; set; }

    public int? LicCond { get; set; }

    public int? CondId { get; set; }

    public virtual Condition? Cond { get; set; }

    public virtual LicenceDetail? LicCondNavigation { get; set; }
}
