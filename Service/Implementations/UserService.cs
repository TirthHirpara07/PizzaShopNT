using AspNetCoreHero.ToastNotification.Abstractions;
using Entity.Bean;
using Entity.Helper;
using Entity.ViewModal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class UserService : IUserService
{
    private readonly INotyfService _notyf;
   private IUserRepo _repo;
   private readonly IMailService _mail;
   private readonly IHttpContextAccessor _httpcontext;
   private readonly IWebHostEnvironment _env;

   public UserService(IUserRepo repo, IHttpContextAccessor httpcontext, INotyfService notyf,IMailService mail,IWebHostEnvironment env)
   {
      _repo = repo;
      _httpcontext = httpcontext;
      _notyf = notyf;
      _mail = mail;
      _env= env;
   }
    public  (List<ShowUser>,int) GetUsers(Pagination p)
    {
        var ( list, count) = _repo.GetUsers(p.sortColumn, p.currOrder, p.page,p.pageSize, p.search);
        return (list,count);
    }

    public async Task<Pagination> UpdatePageDetails(Pagination p)
    {
        p.maxPage = (int)Math.Ceiling(((double)p.count / p.pageSize));
        p.start = p.page * p.pageSize + 1;
        p.end = (((p.page * p.pageSize) + p.pageSize)>=p.count)? p.count :(p.page * p.pageSize) + p.pageSize;
        return p;
    }
    public async Task<RedirectResult> AddUser(AddUserBean user)
    {
        string filename="";
        string filepath="";
        if(user.UserImg != null){
            string folder = Path.Combine(_env.WebRootPath,"ProfilePhotos");
            filename = Guid.NewGuid().ToString() + "_" + user.UserImg.FileName;
            filepath = Path.Combine(folder,filename);
        }
        bool isadded =await _repo.AddUser(user,filename);
        if(isadded){
            user.UserImg.CopyTo(new FileStream(filepath,FileMode.Create));
            // SendCreateUserMail(user.Email, user.Username, bean.Password);
            string emailBody =await getMailBody(user.Username, user.Password);
            _mail.SendEmail(user.Email,emailBody);
            _notyf.Success("User Added Successfully");
        } 
        else _notyf.Error("Account Already Exist with Email");
        return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
    }
    public async Task<string> getMailBody(string Username,string Password){
        string emailBody = await _mail.GetEmailBodyAsync("AddUser.html");
        emailBody = emailBody.Replace("{{Username}}",Username);
        emailBody = emailBody.Replace("{{Password}}",Password);
        return emailBody;
    }

    public async Task<EditUserBean> GetUserDetails(int id)
    {
         return await _repo.GetUserDetails(id);
    }

    public async Task<RedirectResult> UpdateUser(EditUserBean user)
    {
        string filename="";
        string filepath="";
        if(user.NewUserImg != null){
            string folder = Path.Combine(_env.WebRootPath,"ProfilePhotos");
            filename = Guid.NewGuid().ToString() + "_" + user.NewUserImg.FileName;
            filepath = Path.Combine(folder,filename);
        }
      bool isupdated =  await _repo.UpdateUser(user,filename);
      if(isupdated) {
        if(filepath != "") user.NewUserImg.CopyTo(new FileStream(filepath,FileMode.Create));
        _notyf.Success("User Updated Succefully");
      }
      else _notyf.Error("User Not Updated");
      return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
    }
    public async Task<RedirectResult> DeleteUser(int id)
    {
      bool isdeleted =  await _repo.DeleteUser(id);
      if(isdeleted) {
        _notyf.Success("User Deleted Succefully");
      }
      else _notyf.Error("User Not Deleted");
      return new RedirectResult { shouldRedirect = true, controller = "User", action = "index" };
    }

}