using Microsoft.AspNetCore.Mvc;
using Entity.Bean;
using Service.Interfaces;
using System.Threading.Tasks;
namespace Web.Controllers;

public class LoginController : Controller
{
    private ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> SignIn(UserLoginBean user)
    {
        var result = await _service.SignIn(user);
        ViewBag.username = HttpContext.Session.GetString("Username");
        return RedirectToAction(result.action, result.controller);
    }

    [HttpGet]
    public IActionResult ForgotPassword(string email)
    {
        ForgotPasswordBean user = new ForgotPasswordBean { Email = email };
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordBean user)
    {
        var result = await _service.SendLink(user.Email);
        return RedirectToAction(result.action, result.controller);
    }

    public async Task<IActionResult> ResetPassword(string email, string token)
    {
        ResetPasswordbean user = await _service.ValidateLink(email, token);
        return View(user);
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordbean user)
    {
        var result = await _service.ResetPassword(user);
        return RedirectToAction(result.action, result.controller);
    }

    public async Task<IActionResult> LogOut()
    {
        var result = await _service.LogOut();
        return RedirectToAction(result.action, result.controller);
    }

    public async Task<IActionResult> ShowError(int statusCode)
    {
        var errorMessages = new Dictionary<int, string>
        {
            { 400, "Bad Request: The request was invalid." },
            { 401, "Unauthorized: You need to log in to access this page." },
            { 403, "Forbidden: You don't have permission to view this page." },
            { 404, "Not Found: The page you are looking for does not exist." },
            { 500, "Internal Server Error: Something went wrong on our end." }
        };
        ViewData["StatusCode"] = statusCode;
        ViewData["ErrorMessage"] = errorMessages.ContainsKey(statusCode)? errorMessages[statusCode] 
    : "An unexpected error occurred.";;
        return View();
    }


}