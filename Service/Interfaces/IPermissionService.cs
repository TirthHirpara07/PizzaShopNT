using Entity.Bean;
using Entity.Helper;

namespace Service.Interfaces;

public interface IPermissionService
{
    public Task<List<PermissionBean>> GetPermissions(string role);
    public Task<RedirectResult> UpdatePermission(List<PermissionBean> list);

}
