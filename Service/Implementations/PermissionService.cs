using AspNetCoreHero.ToastNotification.Abstractions;
using Entity.Bean;
using Entity.Helper;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepo _repo;
     private readonly INotyfService _notyf;
    public PermissionService(IPermissionRepo repo,INotyfService notyf)
    {
        _repo = repo;
        _notyf=notyf;
    }
    public async Task<List<PermissionBean>> GetPermissions(string role)
    {
        return await _repo.GetPermissions(role);
    }

    public async Task<RedirectResult> UpdatePermission(List<PermissionBean> list)
    {
        bool isupdated = await _repo.UpdatePermission(list);
        if(isupdated){
            _notyf.Success("Permission Updated Successfully");
        }
        else _notyf.Error("Permission not Updated");
        return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
    }
}
