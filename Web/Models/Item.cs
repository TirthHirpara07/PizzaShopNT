using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Item
{
    public int Itemid { get; set; }

    public int Categoryid { get; set; }

    public string Itemtype { get; set; } = null!;

    public string Itemname { get; set; } = null!;

    public decimal Itemrate { get; set; }

    public int Itemquantity { get; set; }

    public bool Isavailable { get; set; }

    public bool Defaulttax { get; set; }

    public string? Description { get; set; }

    public string? ItemImage { get; set; }

    public int Modifierid { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public bool? Isfavourite { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Favouriteitem> Favouriteitems { get; set; } = new List<Favouriteitem>();

    public virtual ICollection<Kot> Kots { get; set; } = new List<Kot>();

    public virtual Modifiergroup Modifier { get; set; } = null!;

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
