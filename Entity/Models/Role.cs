using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string? Rolename { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
