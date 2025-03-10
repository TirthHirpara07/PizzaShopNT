using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Table
{
    public int Tableid { get; set; }

    public int Sectionid { get; set; }

    public string Tablename { get; set; } = null!;

    public bool Status { get; set; }

    public int Customerid { get; set; }

    public int Capacity { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Kot> Kots { get; set; } = new List<Kot>();

    public virtual Section Section { get; set; } = null!;
}
