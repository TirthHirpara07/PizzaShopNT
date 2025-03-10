using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Deptname { get; set; }

    public bool Isdeleted { get; set; }

    public int? Modifiedby { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public virtual User? ModifiedbyNavigation { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
