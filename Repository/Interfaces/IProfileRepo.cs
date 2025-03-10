using Entity.Bean;
using Entity.Helper;
namespace Repository.Interfaces;

public interface IProfileRepo
{
    public Task<ProfileRequestBean> GetUserDetails(string email);
    public Task<List<PlaceDTO>> GetRoles();
    public Task<List<PlaceDTO>> GetCountries();
    public Task<List<PlaceDTO>> GetStates(int countryid);
    public Task<List<PlaceDTO>> GetCities(int stateid);
    public Task<bool> UpdateProfile(ProfileRequestBean user,int userid);
    public Task<bool> ChangePassword(ChangePasswordBean bean,int userid);
}