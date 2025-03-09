using System.ComponentModel.DataAnnotations;
namespace Entity.Bean;
public class ChangePasswordBean
{
    [Required(ErrorMessage = "Current Password Required")]
    public required string CurrentPassword { get; set; }

    [Required(ErrorMessage = "New Password Required")]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$",
    //     ErrorMessage = "Password must be at least 8 characters,\n including 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password Required")]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

}
