using Entity.Helper;
using Entity.Models;
using Entity.ViewModal;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class ItemRepo : IItemRepo
{
    private readonly PizzaShopContext _context;

    public ItemRepo(PizzaShopContext context)
    {
        _context = context;
    }



    public async Task<List<ShowCategory>> GetCategories()
    {
        List<ShowCategory> list = new List<ShowCategory>();
        var dbuser = _context.Categories.Where(a => a.Isdeleted == false).OrderBy(m=>m.Id).ToList();
        if (dbuser != null)
        {
            foreach (var category in dbuser)
            {
                ShowCategory c = new ShowCategory()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
                list.Add(c);
            }
        }
        return list;
    }

    public async Task<bool> AddCategory(ShowCategory category)
    {
        var dbuser = _context.Categories.Where(a => a.Isdeleted == false && a.Name == category.Name).FirstOrDefault();
        if (dbuser != null)
        {
            return false;
        }
        else
        {
            Category c = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                Createddate = DateTime.Now,
                Modifieddate = DateTime.Now
            };
            _context.Categories.Add(c);
            _context.SaveChanges();
            return true;

        }
    }

    public async Task<bool> UpdateCategory(ShowCategory category)
    {
        var dbuser = _context.Categories.Where(a => a.Isdeleted == false && a.Id == category.Id).FirstOrDefault();
        if (dbuser != null)
        {
            dbuser.Name = category.Name;
            dbuser.Description = category.Description;
            dbuser.Modifieddate = DateTime.Now;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteCategory(int id)
    {
       var dbuser = _context.Items.Include(a=>a.Category).Where(a => a.Isdeleted == false && a.Categoryid == id).ToList();
        if (dbuser != null)
        {
            foreach(var item in dbuser){
                item.Isdeleted = true;
                item.ModifiedDate = DateTime.Now;
                item.Category.Isdeleted = true;
            }
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public  (List<ShowItem>,int) GetItemsByCategory(int categoryid,int page,int pageSize, string search)
    {
        List<ShowItem> list = new List<ShowItem>();
         int count=0;
        if(search==null) search="";
        var dbuser = _context.Items.Include(m=>m.Category).Where(a =>( a.Isdeleted == false && a.Categoryid == categoryid) && ( a.Itemname.Contains(search) || a.Itemtype.Contains(search) ));
        if(dbuser != null){

             count= dbuser.Count();
            dbuser = dbuser.Skip(page*pageSize ).Take(pageSize);
            foreach (var item in dbuser)
            {
                ShowItem c = new ShowItem()
                {
                    Id = item.Itemid,
                    Name = item.Itemname,
                    ItemType = Enum.Parse<ItemType>(item.Itemtype),
                    Rate = (float)item.Itemrate,
                    Quantity = item.Itemquantity,
                    Available = item.Isavailable,
                    ItemImg = item.ItemImage
                };
                list.Add(c);
            }
        }
        return (list,count);
    }
}
