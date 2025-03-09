using System.ComponentModel.DataAnnotations;

namespace Entity.Bean;

public class PermissionBean
{
    [Required]
    public string RoleName { get; set; }

    public string DepartmentName { get; set; }

    public bool CanView { get; set; }
    
    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }
}
