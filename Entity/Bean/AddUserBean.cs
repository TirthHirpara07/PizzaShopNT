using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Entity.Bean;

public class AddUserBean
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

    [Required(ErrorMessage ="Password Required")]
    [StringLength(15)]
    public string? Password { get; set; }

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

    [DataType(DataType.Upload)]
    public IFormFile UserImg { get; set; }
}
