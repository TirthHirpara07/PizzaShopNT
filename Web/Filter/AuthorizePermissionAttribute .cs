using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Implementations;

public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _deptName;
    private readonly string _permissionType;

    public AuthorizePermissionAttribute(string deptName, string permissionType)
    {
        _deptName = deptName;
        _permissionType = permissionType;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        string claimName = $"Dept-{_deptName}-{_permissionType}";

        bool hasPermission = user.Claims.Any(c => c.Type == claimName && c.Value == "True");

        if (!hasPermission)
        {        
             context.Result = new RedirectToActionResult("ShowError","login", new { statusCode = 403});
        }

    }

}