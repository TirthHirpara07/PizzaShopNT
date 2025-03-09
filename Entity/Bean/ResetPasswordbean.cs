using System.ComponentModel.DataAnnotations;

namespace Entity.Bean;

public class ResetPasswordbean
{
    [Required(ErrorMessage = "New Password Required")]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$",
    //     ErrorMessage = "Password must be at least 8 characters,\n including 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]
     public string Password { get; set; }

    [Compare("Password", ErrorMessage="The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string email { get; set; }

     [Required(ErrorMessage = "invalid link")]
    public string token { get; set; }
}
