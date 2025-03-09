using System;
using System.Collections;
using System.Collections.Generic;

namespace Web.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public string Paymentmode { get; set; } = null!;

    public string? Cardno { get; set; }

    public BitArray[]? Upiid { get; set; }

    public bool? Status { get; set; }

    public int? Totalamount { get; set; }

    public int? Invoiceid { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
