using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Section
{
    public int Sectionid { get; set; }

    public string? Sectionname { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
