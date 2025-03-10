using Entity.Bean;

namespace Repository.Interfaces;

public interface ILoginRepo
{
   public Task<(int? userid , string? role, string? username,string? userimg)> SignIn(UserLoginBean bean);
    public  Task<bool> ValidateUser(string email);
    public  Task SetToken(string email , string token);

    public Task<bool> ValidateToken(string email,string token);
    public Task<bool> ResetPassword(ResetPasswordbean user);

}