using Entity.Bean;
using Entity.Helper;

namespace Service.Interfaces;

public interface ILoginService
{
    public Task<RedirectResult> SignIn(UserLoginBean user);

    public Task<RedirectResult> SendLink(string Email);
    public Task<ResetPasswordbean> ValidateLink(string email,string token);
    public Task<RedirectResult> ResetPassword(ResetPasswordbean user);
    public Task<RedirectResult> LogOut();
}