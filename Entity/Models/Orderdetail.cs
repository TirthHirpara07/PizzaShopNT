using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Orderdetail
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Itemid { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public int? Itemquantity { get; set; }

    public int? Preparedquantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
