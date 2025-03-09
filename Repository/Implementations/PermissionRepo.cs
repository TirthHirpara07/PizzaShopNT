using Entity.Bean;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;


namespace Repository.Implementations;

public class PermissionRepo : IPermissionRepo
{
    private readonly PizzaShopContext _context;
    public PermissionRepo(PizzaShopContext context)
    {
        _context = context;
    }
    public async Task<List<PermissionBean>> GetPermissions(string role)
    {
        var dbuser = _context.Permissions.Include(a => a.Role).Include(b => b.Dept).Where(m => m.Role.Rolename == role).ToList();
        List<PermissionBean> list = new List<PermissionBean>();
        if (dbuser != null && dbuser.Count > 0)
        {
            foreach (var p in dbuser)
            {
                PermissionBean pb = new PermissionBean
                {
                    RoleName = p.Role.Rolename,
                    DepartmentName = p.Dept.Deptname,
                    CanView = p.Canview,
                    CanEdit = p.Canedit,
                    CanDelete = p.Candelete
                };
                list.Add(pb);
            }
        }
        return list;
    }

    public async Task<bool> UpdatePermission(List<PermissionBean> list)
    {
        string role = list.FirstOrDefault().RoleName;
        var dbuser = _context.Permissions.Include(a => a.Role).Include(b => b.Dept).Where(m => m.Role.Rolename == role);
        if (dbuser != null)
        {
            foreach (var permission in dbuser)
            {
                foreach (PermissionBean p in list)
                {
                    if (permission.Dept.Deptname == p.DepartmentName)
                    {
                        permission.Canview = p.CanView;
                        permission.Canedit = p.CanEdit;
                        permission.Candelete = p.CanDelete;
                    }
                }
            }
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
