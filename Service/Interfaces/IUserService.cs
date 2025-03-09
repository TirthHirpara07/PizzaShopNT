using Entity.Bean;
using Entity.Helper;
using Entity.ViewModal;

namespace Service.Interfaces;

public interface IUserService
{
    public (List<ShowUser>,int) GetUsers(Pagination p);
    public Task<Pagination> UpdatePageDetails(Pagination p);
    public Task<RedirectResult> AddUser(AddUserBean user);
     public Task<EditUserBean> GetUserDetails(int id);
     public Task<RedirectResult> UpdateUser(EditUserBean user);
     public Task<RedirectResult> DeleteUser(int id);
}