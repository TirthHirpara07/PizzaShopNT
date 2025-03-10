using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Permission
{
    public int Permissionid { get; set; }

    public int Roleid { get; set; }

    public int Deptid { get; set; }

    public bool Canview { get; set; }

    public bool Canedit { get; set; }

    public bool Candelete { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime[] Createddate { get; set; } = null!;

    public DateTime[] Modifieddate { get; set; } = null!;

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
