using System.Security.Claims;
using Entity.Bean;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(int userId, string role,string username,string email, List<PermissionBean> permissions);
}