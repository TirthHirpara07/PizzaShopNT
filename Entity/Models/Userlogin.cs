using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Userlogin
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Hashpassword { get; set; }

    public int? Roleid { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Userid { get; set; }

    public string? Token { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
