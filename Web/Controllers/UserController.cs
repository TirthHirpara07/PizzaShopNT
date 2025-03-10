using System.Threading.Tasks;
using Entity.Bean;
using Entity.Helper;
using Entity.ViewModal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Implementations;
using Service.Interfaces;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _service;
     private readonly IProfileService _profileservice;
    public UserController(IUserService service,IProfileService profileservice){
        _service = service;
        _profileservice = profileservice;
    }

    [AuthorizePermission("User","CanView")]
    public async Task<IActionResult> Index(Pagination p)
    {
         var (data, count)  = _service.GetUsers(p);
        p.count= count;
        ViewBag.pageContent =await _service.UpdatePageDetails(p);
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> TableSize(Pagination p)
    {
         var (data, count)   = _service.GetUsers(p);
        p.count = count;
        ViewBag.pageContent =await _service.UpdatePageDetails(p);
        return PartialView(data);
    }

    public async Task<IActionResult> AddUser()
    {
         ViewData["CountryName"] = new SelectList(await _profileservice.GetCountries(), "Id", "Name");
         return View();
    }    
    [HttpPost]
    [AuthorizePermission("User","CanEdit")]
    public async Task<IActionResult> AddUser(AddUserBean user)
    {
        var result = await _service.AddUser(user);
        return RedirectToAction (result.action,result.controller);
    }   

    [AuthorizePermission("User","CanEdit")] 
    public async Task<IActionResult> UpdateUser(int id)
    {
        EditUserBean predata = await _service.GetUserDetails(id);
        ViewBag.PreEdit = predata;
        ViewData["RoleName"] = new SelectList(await _profileservice.GetRoles(), "Id", "Name");
        ViewData["CountryName"] = new SelectList(await _profileservice.GetCountries(), "Id", "Name");
        ViewData["StateName"] = new SelectList(await _profileservice.GetStates((int)predata.Country), "Id", "Name");
        ViewData["CityName"] = new SelectList(await _profileservice.GetCities((int)predata.State), "Id", "Name");
        return View();
    }    

    [HttpPost]
    [AuthorizePermission("User","CanEdit")]
    public async Task<IActionResult> UpdateUser(EditUserBean user)
    {
        var result = await _service.UpdateUser(user);
        return RedirectToAction (result.action,result.controller);
    } 

    [AuthorizePermission("User","CanEdit")]
    public async Task<IActionResult> deleteuser(int id){
        var result = await _service.DeleteUser(id);
        return RedirectToAction (result.action,result.controller);
    }  



        
}