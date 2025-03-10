using Entity.Bean;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Implementations;
using Service.Interfaces;

namespace Web.Controllers;

public class RolePermission : Controller
{

    private readonly IPermissionService _service;
    private readonly IProfileService _profileservice;
    public RolePermission(IProfileService profileservice, IPermissionService service)
    {
         _profileservice = profileservice;
         _service = service;
    }

    [AuthorizePermission("RolePermission","CanView")]
    public async Task<IActionResult> Index()
    {
        ViewData["RoleName"] = new SelectList(await _profileservice.GetRoles(), "Id", "Name");
        return View();
    }
    public async Task<IActionResult> Permission(string role)
    {
        List<PermissionBean> list= await _service.GetPermissions(role);
        return View(list);
    }
    
    [AuthorizePermission("RolePermission","CanEdit")]
    public async Task<IActionResult> UpdatePermission(List<PermissionBean> list)
    {
         var result = await _service.UpdatePermission(list);
        return RedirectToAction (result.action,result.controller);
    }
}