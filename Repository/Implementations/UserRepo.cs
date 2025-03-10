using Entity.Bean;
using Entity.Models;
using Entity.ViewModal;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class UserRepo : IUserRepo
{

    private readonly PizzaShopContext _context;

    public UserRepo(PizzaShopContext context)
    {
        _context = context;
    }
    public  (List<ShowUser>,int ) GetUsers(string sortColumn, string currOrder, int page, int pageSize,string search)
    {
        if(search==null) search="";
        var dbuser = _context.Userlogins.Include(a => a.User).Include(b => b.Role).Where(m => (m.Isdeleted == false ) && (m.Email.Contains(search) || m.User.Firstname.Contains(search) || m.User.Lastname.Contains(search) || m.User.Phoneno.Contains(search) || m.User.Role.Rolename.Contains(search)));
        switch (sortColumn)
        {
            case "Role":
                dbuser = (currOrder == "desc") ? dbuser.OrderByDescending(a => a.Role.Rolename) : dbuser.OrderBy(a => a.Role.Rolename);
                break;
            default:
                dbuser = (currOrder == "asc") ? dbuser.OrderBy(a => a.User.Firstname) : dbuser.OrderByDescending(a => a.User.Firstname);
                break;
        }
        int count= dbuser.Count();
        dbuser = dbuser.Skip(page*pageSize ).Take(pageSize);
        List<ShowUser> users = new List<ShowUser>();
        if (dbuser != null)
        {
            foreach (var usr in dbuser)
            {
                ShowUser user = new ShowUser();
                user.UserId = usr.Userid;
                user.Name = usr.User.Firstname + " " + usr.User.Lastname;
                user.Email = usr.Email;
                user.PhoneNo = usr.User.Phoneno;
                user.Role = usr.Role.Rolename;
                user.Status = usr.User.Status;
                user.UserImg = (usr.User.Userimg == null)? "Default_pfp.png":usr.User.Userimg;
                users.Add(user);
            }
        }
        return (users,count);
    }

    public async Task<bool> AddUser(AddUserBean user,string filename)
    {
        
        var dbuser = _context.Userlogins.FirstOrDefault(m => m.Email == user.Email);
        if (dbuser == null)
        {
            Useraddress add = new Useraddress()
            {
                Address = user.Address,
                Pincode = user.ZipCode,
                Isdeleted = false,
                Country = user.Country,
                State = user.State,
                City = user.City
            };
            _context.Useraddresses.Add(add);
            _context.SaveChanges();

            User newuser = new User()
            {
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Phoneno = user.PhoneNo,
                Roleid = user.Role,
                Isdeleted = false,
                Username = user.Username,
                Status = true,
                Addressid = add.Id,
                Userimg = filename
            };
            _context.Users.Add(newuser);
            _context.SaveChanges();
            Userlogin newlogin = new Userlogin()
            {
                Email = user.Email,
                Hashpassword = LoginRepo.encryptpass(user.Password),
                Isdeleted = false,
                Roleid = user.Role,
                Userid = newuser.Userid
            };
            _context.Userlogins.Add(newlogin);
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<EditUserBean> GetUserDetails(int id)
    {
        var dbuser = _context.Userlogins.Include(m => m.User).ThenInclude(b => b.Address).
            Where(m => m.Isdeleted == false && m.Userid == id).FirstOrDefault();


        EditUserBean predata = new EditUserBean()
        {
            FirstName = dbuser.User.Firstname,
            LastName = dbuser.User.Lastname,
            Username = dbuser.User.Username,
            Email = dbuser.Email,
            Role = dbuser.Roleid,
            PhoneNo = dbuser.User.Phoneno,
            Status = dbuser.User.Status,
            Country = (int)dbuser.User.Address.Country,
            State = (int)dbuser.User.Address.State,
            City = (int)dbuser.User.Address.City,
            Address = dbuser.User.Address.Address,
            ZipCode = dbuser.User.Address.Pincode,
            UserImg= dbuser.User.Userimg
        };
        return predata;
    }

    public async Task<bool> UpdateUser(EditUserBean user,string filename)
    {
        var dbuser = _context.Userlogins.Include(a => a.User).Include(b => b.User.Address).Where(m => m.Email == user.Email).FirstOrDefault();
        if (dbuser != null)
        {
            dbuser.User.Firstname = user.FirstName;
            dbuser.User.Lastname = user.LastName;
            dbuser.User.Username = user.Username;
            dbuser.User.Phoneno = user.PhoneNo;
            dbuser.User.Status = user.Status;
            dbuser.User.Address.Country = user.Country;
            dbuser.User.Address.State = user.State;
            dbuser.User.Address.City = user.City;
            dbuser.User.Address.Address = user.Address;
            dbuser.User.Address.Pincode = user.ZipCode;
            dbuser.Roleid = user.Role;
            dbuser.User.Roleid = user.Role;
            if(filename != ""){
                dbuser.User.Userimg=filename;
            }
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
         var dbuser = _context.Userlogins.Include(a => a.User).ThenInclude(b => b.Address).FirstOrDefault(m => m.User.Userid == id);
        if (dbuser != null)
        {
            dbuser.Isdeleted = true;
            dbuser.User.Isdeleted = true;
            dbuser.User.Address.Isdeleted = true;
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}