using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Order
{
    public int Id { get; set; }

    public int Customerid { get; set; }

    public bool Status { get; set; }

    public DateTime[] Orderdate { get; set; } = null!;

    public TimeOnly? Orderduration { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public int? Paymentid { get; set; }

    public int? Totalamount { get; set; }

    public int? Modifierid { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Invoice? Invoice { get; set; }

    public virtual ICollection<Kot> Kots { get; set; } = new List<Kot>();

    public virtual Modifier? Modifier { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<Ordertax> Ordertaxes { get; set; } = new List<Ordertax>();

    public virtual Payment? Payment { get; set; }
}
