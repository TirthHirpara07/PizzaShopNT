using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Modifiergroup
{
    public int Id { get; set; }

    public string Modifiergroupname { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public string? Modifiergroupdescription { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Modifier> Modifiers { get; set; } = new List<Modifier>();
}
