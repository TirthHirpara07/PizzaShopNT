
using System.Security;
using AspNetCoreHero.ToastNotification.Abstractions;
using Entity.Bean;
using Entity.Helper;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;
namespace Service.Implementations;
public class LoginService : ILoginService
{
   private readonly INotyfService _notyf;
   private ILoginRepo _repo;
   private readonly IMailService _mail;
   private readonly IPermissionService _permissionservice;
   private readonly IHttpContextAccessor _httpcontext;
   private readonly IJwtService _jwtService;

   public LoginService(ILoginRepo repo, IHttpContextAccessor httpcontext, INotyfService notyf, IMailService mail, IPermissionService permissionservice,IJwtService jwtservice
   )
   {
      _repo = repo;
      _httpcontext = httpcontext;
      _notyf = notyf;
      _mail = mail;
      _permissionservice = permissionservice;
      _jwtService = jwtservice;
   }

   public async Task<RedirectResult> SignIn(UserLoginBean user)
   {

      var (Userid, role, username, userimg) = await _repo.SignIn(user);
      if (Userid <= 0)
      {
         _notyf.Error("Invalid Credentials");
         return new RedirectResult { shouldRedirect = true, controller = "Login", action = "index" };
      }
      var permissions =await _permissionservice.GetPermissions(role);
      var token = _jwtService.GenerateJwtToken((int)Userid, role, username,  user.Email ,permissions);

      CookieOptions cookie = new CookieOptions
      {
         HttpOnly = true,
         Secure = true,
         Expires = user.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(1) 
      };

      _httpcontext.HttpContext.Response.Cookies.Append("JwtToken", token, cookie);  // Store JWT in Cookie

      if (!user.RememberMe)
      {
         _httpcontext.HttpContext.Session.SetString("JwtToken", token);  //  Store JWT in Session if Remember Me is off
      }
      _notyf.Success("Logged In");
      return new RedirectResult { shouldRedirect = true, controller = "Login", action = "index" };
   }

   public async Task<RedirectResult> SendLink(string Email)
   {
      bool isvalid = await _repo.ValidateUser(Email);
      if (isvalid)
      {
         string emailBody = await _mail.GetEmailBodyAsync("ForgotPass.html");
         var guid = Guid.NewGuid().ToString();
         _repo.SetToken(Email, guid);
         string? resetPasswordLink = $"http://localhost:5063/login/ResetPassword?email={Email}&token={guid}";
         emailBody = emailBody.Replace("{{reset_link}}", resetPasswordLink);
         _mail.SendEmail(Email, emailBody);
         _notyf.Success("Email Sent!");
         return new RedirectResult { shouldRedirect = true, controller = "Login", action = "index" };
      }
      _notyf.Error("Email Does not Exist");
      return new RedirectResult { shouldRedirect = true, controller = "Login", action = "ForgotPassword" };

   }



   public async Task<ResetPasswordbean> ValidateLink(string email, string token)
   {
      bool isvalid = await _repo.ValidateToken(email, token);
      if (isvalid)
      {
         return new ResetPasswordbean { email = email, token = token };
      }
      else
      {
         return new ResetPasswordbean { email = string.Empty, token = string.Empty };
      }
   }

   public async Task<RedirectResult> ResetPassword(ResetPasswordbean user)
   {
      if (user.email.Equals(string.Empty) || user.token.Equals(string.Empty))
      {
         _notyf.Error("Reset Password Link Expires");
         return new RedirectResult { shouldRedirect = true, controller = "Login", action = "index" };
      }
      bool isvalid = await _repo.ResetPassword(user);
      if (isvalid)
      {
         _notyf.Success("Password Updated Successfully");
         return new RedirectResult { shouldRedirect = true, controller = "Login", action = "Index" };
      }
      _notyf.Success("Invalid Credentials");
      return new RedirectResult { shouldRedirect = true, controller = "Login", action = "Index" };
   }

   public async Task<RedirectResult> LogOut()
   {
      _httpcontext.HttpContext.Session.Clear();
      _httpcontext.HttpContext.Response.Cookies.Delete("JwtToken");
      return new RedirectResult { shouldRedirect = true, controller = "Login", action = "Index" };
   }
}