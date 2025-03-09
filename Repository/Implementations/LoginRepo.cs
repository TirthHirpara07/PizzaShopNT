using System.Text;
using Entity.Bean;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class LoginRepo : ILoginRepo
{

    private readonly PizzaShopContext _context;

    public LoginRepo(PizzaShopContext context)
    {
        _context = context;
    }


    public async Task<(int? userid, string? role, string? username,string userimg)> SignIn(UserLoginBean bean)
    {
        var dbuser = _context.Userlogins.Include(a => a.User).Include(b => b.Role).Where(m => m.Email == bean.Email && m.Hashpassword == encryptpass(bean.Password) && m.Isdeleted == false).FirstOrDefault();
        if(dbuser == null){
            return (0,String.Empty,String.Empty,"https://i.ibb.co/JW8s0S7m/Default-pfp-svg.png");
        }
        return (dbuser.Userid, dbuser.Role.Rolename, dbuser.User.Username, dbuser.User.Userimg);
    }
    
    // Validates User
    public async Task<bool> ValidateUser(string email)
    {
        return _context.Userlogins.Any(m=>m.Email == email && m.Isdeleted == false);
    }

    public async Task SetToken(string email , string token)
    {
        var dbuser = _context.Userlogins.FirstOrDefault(m=>m.Email == email && m.Isdeleted == false);
        if(dbuser != null){
            dbuser.Token = token;
            dbuser.Modifieddate = DateTime.Now;
            _context.SaveChanges();
        }
        return; 
    }

    public async Task<bool> ValidateToken(string email,string token)
    {
        var dbuser = _context.Userlogins.FirstOrDefault(m => m.Isdeleted == false && m.Email == email);
        if (dbuser != null)
        {
            var isvalid = DateTime.Now.Subtract((DateTime)dbuser.Modifieddate);
            if (isvalid.Minutes < 5)
            {
                return true;
            }
        }
        return false;
    } 

    public async Task<bool> ResetPassword(ResetPasswordbean user)
    {
        var dbuser = _context.Userlogins.FirstOrDefault(m => m.Isdeleted == false && m.Email == user.email && m.Token == user.token);
        if(dbuser!=null){
            dbuser.Hashpassword = encryptpass(user.Password);
            dbuser.Modifieddate = DateTime.Now;
            _context.SaveChanges();
            return true;
        }
        return false;
    }



    // EncryptionAlgorithm/Decryption
    public static string encryptpass(string password)
    {
        string msg = "";
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        msg = Convert.ToBase64String(encode);
        return msg;
    }
}