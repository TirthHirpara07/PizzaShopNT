using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Customerwaiting
{
    public int Tokenid { get; set; }

    public int Customerid { get; set; }

    public TimeOnly? Waitingtime { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public int? Noofpeople { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
