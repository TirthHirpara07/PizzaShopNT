using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Ordertax
{
    public int Id { get; set; }

    public int? Orderid { get; set; }

    public int? Taxid { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Tax? Tax { get; set; }
}
