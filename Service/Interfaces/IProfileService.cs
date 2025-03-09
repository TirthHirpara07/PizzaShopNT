using Entity.Bean;
using Entity.Helper;

namespace Service.Interfaces;

public interface IProfileService
{
    public Task<ProfileRequestBean> GetUserDetails();

    public Task<List<PlaceDTO>> GetCountries();
    public Task<List<PlaceDTO>> GetStates(int countryid);
    public Task<List<PlaceDTO>> GetCities(int stateid);
    public Task<List<PlaceDTO>> GetRoles();
    public Task<RedirectResult> UpdateProfile(ProfileRequestBean user);
    public Task<RedirectResult> ChangePassword(ChangePasswordBean bean);

}