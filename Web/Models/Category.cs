using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<Favouriteitem> Favouriteitems { get; set; } = new List<Favouriteitem>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
