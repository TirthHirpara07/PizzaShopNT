using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Favouriteitem
{
    public int Id { get; set; }

    public int? Itemid { get; set; }

    public int? Categoryid { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Item? Item { get; set; }
}
