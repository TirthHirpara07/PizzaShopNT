﻿using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<Useraddress> Useraddresses { get; set; } = new List<Useraddress>();
}
