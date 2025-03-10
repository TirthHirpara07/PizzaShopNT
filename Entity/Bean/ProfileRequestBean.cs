using System.ComponentModel.DataAnnotations;

namespace Entity.Bean;

public class ProfileRequestBean
{
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

    [Required(ErrorMessage ="Country Required")]
    public int? Country { get; set; }

    [Required(ErrorMessage ="State Required")]
    public int? State { get; set; }

    [Required(ErrorMessage ="City Required")]
    public int? City { get; set; }

     [Required(ErrorMessage ="Address Required")]
    [StringLength(60)]
    public string? Address { get; set; }

    [Required(ErrorMessage ="ZipCode Required")]
    [StringLength(6)]
    public string? ZipCode { get; set; }
    public string? UserImg { get; set; }

}
