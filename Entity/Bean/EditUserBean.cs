using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Entity.Bean;

public class EditUserBean
{
     [Required(ErrorMessage ="Email Required")]
     [StringLength(100)]
    public string Email { get; set; }

    [Required(ErrorMessage ="Firstname Required")]
    [StringLength(20)]
    public string? FirstName { get; set; }

    [Required(ErrorMessage ="Lastname Required")]
    [StringLength(20)]
    public string? LastName { get; set; }

    [Required(ErrorMessage ="Username Required")]
    [StringLength(20)]
    public string? Username { get; set; }

    [Required(ErrorMessage ="Phone No. Required")]
    [StringLength(10)]
    public string? PhoneNo { get; set; }
    
    [Required(ErrorMessage ="Role Required")]
    public int? Role { get; set; }

    [Required(ErrorMessage ="status Required")]
    public bool? Status { get; set; }

    [Required(ErrorMessage ="Country Required")]
    public int Country { get; set; }

    [Required(ErrorMessage ="State Required")]
    public int State { get; set; }

    [Required(ErrorMessage ="City Required")]
    public int City { get; set; }

    [Required(ErrorMessage ="ZipCode Required")]
    [StringLength(6)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage ="Address Required")]
    [StringLength(60)]
    public string Address { get; set; }
    public string UserImg { get; set; }
    public IFormFile NewUserImg { get; set; }
}
