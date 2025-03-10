using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int? Food { get; set; }

    public int? Service { get; set; }

    public int? Ambience { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public string? Comment { get; set; }

    public virtual Order Order { get; set; } = null!;
}
