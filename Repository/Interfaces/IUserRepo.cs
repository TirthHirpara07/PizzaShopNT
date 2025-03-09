
using Entity.Bean;
using Entity.ViewModal;

namespace Repository.Interfaces;

public interface IUserRepo
{
    public (List<ShowUser>,int) GetUsers(string sortColumn, string currOrder,int page, int pageSize,string search);
    public Task<bool> AddUser(AddUserBean user,string filename);
    public Task<EditUserBean> GetUserDetails(int id);
    Task<bool> UpdateUser(EditUserBean user,string filename);
    public Task<bool> DeleteUser(int id);

}