using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity.Bean;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;

namespace Service.Implementations;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtService( IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {   
        _secretKey = configuration["JwtSettings:SecretKey"];
        _httpContextAccessor = httpContextAccessor;
    }


    public string GenerateJwtToken(int userId, string role,string username,string email, List<PermissionBean> permissions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email)
        };

        // Add permission claims
        foreach (var perm in permissions)
        {
            claims.Add(new Claim($"Dept-{perm.DepartmentName}-CanView", perm.CanView.ToString()));
            claims.Add(new Claim($"Dept-{perm.DepartmentName}-CanEdit", perm.CanEdit.ToString()));
            claims.Add(new Claim($"Dept-{perm.DepartmentName}-CanDelete", perm.CanDelete.ToString()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }



}