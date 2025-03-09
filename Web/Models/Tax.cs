using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Tax
{
    public int Taxid { get; set; }

    public string Taxname { get; set; } = null!;

    public string Taxtype { get; set; } = null!;

    public bool Isenable { get; set; }

    public bool Isdefault { get; set; }

    public double Taxvalue { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<Ordertax> Ordertaxes { get; set; } = new List<Ordertax>();
}
