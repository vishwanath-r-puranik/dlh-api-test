using System;
using System.Collections.Generic;

namespace DLHApi.DAL.Models;

public partial class DlhRequestAudit
{
    public int? Id { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Mvid { get; set; }

    public string? ReqStatus { get; set; }
}
