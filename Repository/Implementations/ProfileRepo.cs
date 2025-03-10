using Entity.Bean;
using Entity.Helper;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class ProfileRepo : IProfileRepo
{
    private readonly PizzaShopContext _context;

    public ProfileRepo(PizzaShopContext context)
    {
        _context = context;
    }
    public async Task<ProfileRequestBean> GetUserDetails(string email)
    {
        var dbuser = _context.Userlogins.Include(m => m.User).ThenInclude(b => b.Address).
            Where(m => m.Isdeleted == false && m.Email == email).FirstOrDefault();


        ProfileRequestBean predata = new ProfileRequestBean()
        {
            FirstName = dbuser.User.Firstname,
            LastName = dbuser.User.Lastname,
            Username = dbuser.User.Username,
            PhoneNo = dbuser.User.Phoneno,
            Country = (int)dbuser.User.Address.Country,
            State = (int)dbuser.User.Address.State,
            City = (int)dbuser.User.Address.City,
            Address = dbuser.User.Address.Address,
            ZipCode = dbuser.User.Address.Pincode,
            UserImg= (dbuser.User.Userimg== null)? "https://i.ibb.co/JW8s0S7m/Default-pfp-svg.png":dbuser.User.Userimg
        };
        return predata;
    }

    public async Task<List<PlaceDTO>> GetRoles()
    {
        return _context.Roles.Select(m => new PlaceDTO
        {
            Id = m.Roleid,
            Name = m.Rolename
        }).ToList();
    }
    public async Task<List<PlaceDTO>> GetCountries()
    {
        return _context.Countries.Select(m => new PlaceDTO
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();
    }

    public async Task<List<PlaceDTO>> GetStates(int countryid)
    {
        return _context.States.Where(m => m.Countryid == countryid).Select(m => new PlaceDTO
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();
    }

    public async Task<List<PlaceDTO>> GetCities(int stateid)
    {
        return _context.Cities.Where(m => m.Stateid == stateid).Select(m => new PlaceDTO
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();
    }
    public async Task<bool> UpdateProfile(ProfileRequestBean user, int userid)
    {
        var dbuser = _context.Users.Include(a => a.Address).FirstOrDefault(m => m.Isdeleted == false && m.Userid == userid);
        if (dbuser != null)
        {
            dbuser.Firstname = user.FirstName;
            dbuser.Lastname = user.LastName;
            dbuser.Username = user.Username;
            dbuser.Phoneno = user.PhoneNo;
            dbuser.Address.Country = user.Country;
            dbuser.Address.State = user.State;
            dbuser.Address.City = user.City;
            dbuser.Address.Address = user.Address;
            dbuser.Address.Pincode = user.ZipCode;
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    public async Task<bool> ChangePassword(ChangePasswordBean bean, int userid)
    {
        var dbuser = _context.Userlogins.FirstOrDefault(m => m.Userid == userid && m.Hashpassword == LoginRepo.encryptpass(bean.CurrentPassword));
        if (dbuser != null)
        {
            dbuser.Hashpassword = LoginRepo.encryptpass(bean.NewPassword);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

}