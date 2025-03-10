using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int? Orderid { get; set; }

    public int? Totalamount { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Order IdNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
