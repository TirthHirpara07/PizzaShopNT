using Entity.Bean;

namespace Repository.Interfaces;

public interface IPermissionRepo
{
    Task<List<PermissionBean>> GetPermissions(string role);
    Task<bool> UpdatePermission(List<PermissionBean> list);
}
