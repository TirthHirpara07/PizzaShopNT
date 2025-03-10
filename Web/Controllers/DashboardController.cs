using System.Diagnostics;
using System.Threading.Tasks;
using Entity.Bean;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Service.Implementations;
using Service.Interfaces;
using Web.Models;

namespace Web.Controllers;

public class DashboardController : Controller
{
    private readonly IProfileService _service;

    public DashboardController(IProfileService service)
    {
        _service = service;
    }

    [AuthorizePermission("Dashboard","CanView")]
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> profile()
    {
        ProfileRequestBean predata = await _service.GetUserDetails();
        ViewBag.PreData = predata;
        ViewData["CountryName"] = new SelectList(await _service.GetCountries(), "Id", "Name");
        ViewData["StateName"] = new SelectList(await _service.GetStates((int)predata.Country), "Id", "Name");
        ViewData["CityName"] = new SelectList(await _service.GetCities((int)predata.State), "Id", "Name");
        return View();
    }
    public IActionResult ChangePasword()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetStates(int countryid)
    {
        var Stateid = await _service.GetStates(countryid);
        return Ok(Stateid);
    }
    public async Task<IActionResult> GetCities(int stateid)
    {
        var Cityid =await _service.GetCities(stateid);
        return Ok(Cityid);
    }

    public async Task<IActionResult> UpdateProfile(ProfileRequestBean user)
    {
        var result = await _service.UpdateProfile(user);
        return RedirectToAction (result.action,result.controller);
    }
    public async Task<IActionResult> ChangePassword(ChangePasswordBean bean)
    {
        var result = await _service.ChangePassword(bean);
        return RedirectToAction (result.action,result.controller);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}