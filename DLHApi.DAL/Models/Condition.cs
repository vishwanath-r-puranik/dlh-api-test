using System;
using System.Collections.Generic;

namespace DLHApi.DAL.Models;

public partial class Condition
{
    public int CondId { get; set; }

    public string? Condition1 { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LicenceCondition> LicenceConditions { get; } = new List<LicenceCondition>();
}
