using System.ComponentModel.DataAnnotations;
namespace Entity.Bean;

public class UserLoginBean
{
    [Required(ErrorMessage ="Email Required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "New Password Required")]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$",
    //     ErrorMessage = "Password must be at least 8 characters,\n including 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]    
    public string Password { get; set; }

    public bool RememberMe { get; set; }

}
    