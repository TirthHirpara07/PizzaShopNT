using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Modifier
{
    public int Id { get; set; }

    public string Modifiername { get; set; } = null!;

    public int Modifiergroupid { get; set; }

    public string Unit { get; set; } = null!;

    public int Modifierrate { get; set; }

    public int Modifierquantity { get; set; }

    public string? Modifierdescription { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
