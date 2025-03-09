using System.ComponentModel.DataAnnotations;

namespace Entity.Bean;

public class ForgotPasswordBean
{
     [Required(ErrorMessage = "Email Required")]
     [StringLength(100)]
     public string Email { get; set; }
}
