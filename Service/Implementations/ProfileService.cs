using AspNetCoreHero.ToastNotification.Abstractions;
using Entity.Bean;
using Entity.Helper;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class ProfileService : IProfileService
{
    private readonly INotyfService _notyf;
   private IProfileRepo _repo;
   private readonly IHttpContextAccessor _httpcontext;

   public ProfileService(IProfileRepo repo, IHttpContextAccessor httpcontext, INotyfService notyf)
   {
      _repo = repo;
      _httpcontext = httpcontext;
      _notyf = notyf;
   }
    public async Task<ProfileRequestBean> GetUserDetails()
    {
        string? email = _httpcontext.HttpContext.Session.GetString("Email");
       return await _repo.GetUserDetails(email);
    }

    public async Task<List<PlaceDTO>> GetRoles()
    {
        return await _repo.GetRoles();
    }
    public async Task<List<PlaceDTO>> GetCountries()
    {
        return await _repo.GetCountries();
    }

    public async Task<List<PlaceDTO>> GetStates(int countryid)
    {
       return await _repo.GetStates(countryid);
    }

    public async Task<List<PlaceDTO>> GetCities(int stateid)
    {
       return await _repo.GetCities(stateid);
    }
    public async Task<RedirectResult> UpdateProfile(ProfileRequestBean user)
    {
      int userid = (int)_httpcontext.HttpContext.Session.GetInt32("Userid");
      bool isupdated =  await _repo.UpdateProfile(user,userid);
      if(isupdated){
         _notyf.Success("Profile Updated Succefully");
         return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
      }
      _notyf.Error("Error!! Profile Not Updated");
      return new RedirectResult { shouldRedirect = true, controller = "Dashbboard", action = "profile" };
    }

    public async Task<RedirectResult> ChangePassword(ChangePasswordBean bean)
    {
        int userid = (int)_httpcontext.HttpContext.Session.GetInt32("Userid");
      bool isupdated =  await _repo.ChangePassword(bean,userid);
      if(isupdated){
         _notyf.Success("Password Changed Succefully");
         return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
      }
      _notyf.Error("Error!! Password Not Changed");
      return new RedirectResult { shouldRedirect = true, controller = "Dashbboard", action = "profile" };
    }
}