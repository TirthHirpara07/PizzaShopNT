using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Useraddress
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string? Pincode { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Modifieddate { get; set; }

    public int? Createdby { get; set; }

    public int? Modifiedby { get; set; }

    public int? City { get; set; }

    public int? State { get; set; }

    public int? Country { get; set; }

    public virtual City? CityNavigation { get; set; }

    public virtual Country? CountryNavigation { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual User? ModifiedbyNavigation { get; set; }

    public virtual State? StateNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
