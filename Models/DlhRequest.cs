using System;
using System.Collections.Generic;

namespace DLHAPI.Models;

public partial class DlhRequest
{
    public int? Id { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Mvid { get; set; }

    public string? ReqStatus { get; set; }
}
