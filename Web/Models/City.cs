using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class City
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public int? Stateid { get; set; }

    public virtual State? State { get; set; }

    public virtual ICollection<Useraddress> Useraddresses { get; set; } = new List<Useraddress>();
}
