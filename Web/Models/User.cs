using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Phoneno { get; set; }

    public int? Roleid { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public string? Username { get; set; }

    public int? Addressid { get; set; }

    public bool? Status { get; set; }

    public string? Userimg { get; set; }

    public virtual Useraddress? Address { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Useraddress> UseraddressCreatedbyNavigations { get; set; } = new List<Useraddress>();

    public virtual ICollection<Useraddress> UseraddressModifiedbyNavigations { get; set; } = new List<Useraddress>();

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
}
