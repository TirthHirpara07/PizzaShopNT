using Microsoft.AspNetCore.Mvc;
using Service.Implementations;

namespace Web.Controllers;

public class MenuController : Controller
{
    public MenuController()
    {

    }

    [AuthorizePermission("Menu","CanView")]
    public async Task<IActionResult> Index()
    {
        return View();
    }
    public async Task<IActionResult> PartialCategory(){
        return PartialView();
    }
}
