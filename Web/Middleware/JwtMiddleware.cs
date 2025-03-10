using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public JwtMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {

            var token = context.Request.Cookies["JwtToken"] ?? context.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                var principal = ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal; //  Set User Context
                    context.Session.SetString("Username", context.User.FindFirst(ClaimTypes.Name).Value);
                    context.Session.SetString("Email", context.User.FindFirst(ClaimTypes.Email).Value);
                    context.Session.SetString("Userid", context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    context.Session.SetString("Role", context.User.FindFirst(ClaimTypes.Role).Value);

                    
                    context.Session.SetString("JwtToken", token);
                    int userId = 0;
                    int.TryParse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                    if (userId > 0)
                    {
                        var cache = context.RequestServices.GetRequiredService<IMemoryCache>();

                        // Try to get profile image from cache
                        if (!cache.TryGetValue($"UserImg_{userId}", out string userimg))
                        {
                            using var scope = context.RequestServices.CreateScope();
                            var dbContext = scope.ServiceProvider.GetRequiredService<PizzaShopContext>();


                            // Fetch from database
                            userimg = await dbContext.Users
                                .Where(u => u.Userid == userId)
                                .Select(u => u.Userimg)
                                .FirstOrDefaultAsync();

                            // Store in cache for 30 minutes
                            if (!string.IsNullOrEmpty(userimg))
                            {
                                cache.Set($"UserImg_{userId}", userimg, TimeSpan.FromMinutes(30));
                            }
                        }

                        context.Session.SetString("UserImg", userimg);
                    }





                    //  Auto Redirect to Default Page if the user is not already on a page
                    if (context.Request.Path == "/" || context.Request.Path == "/login/index")
                    {
                        string defaultPage = GetDefaultPage(principal);
                        context.Response.Redirect(defaultPage);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Redirect($"/login/ShowError?statusCode=500&message={ex.Message}");
            //    Console.WriteLine($"Middleware Exception: {ex.Message}");
        }
        await _next(context);
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);

        try
        {
            SecurityToken validatedToken;
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = _config["JwtSettings:Issuer"],
                ValidAudience = _config["JwtSettings:Audience"],
                ClockSkew = TimeSpan.Zero
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    private string GetDefaultPage(ClaimsPrincipal user)
    {
        string role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        return role switch
        {
            "Admin" => "/Dashboard/index",  // Redirect to AdminController's Dashboard action
            "Account Manager" => "/User/Index",  // Redirect to UserController's Index action
            "Chef" => "/Menu/index",  // Redirect to MenuController's List action
            _ => "/Login/Index"  // Default redirection to LoginController's Index
        };
    }
}