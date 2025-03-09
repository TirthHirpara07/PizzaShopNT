using System;
using System.Collections;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Customername { get; set; } = null!;

    public BitArray Customeremail { get; set; } = null!;

    public string Customerphone { get; set; } = null!;

    public int? Lastorderid { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<Customerwaiting> Customerwaitings { get; set; } = new List<Customerwaiting>();

    public virtual Order? Lastorder { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
