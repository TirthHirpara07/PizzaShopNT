using Entity.Models;
using Entity.ViewModal;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class ModifierRepo:IModifierRepo
{
     private readonly PizzaShopContext _context;

    public ModifierRepo(PizzaShopContext context)
    {
        _context = context;
    }
    
    public async Task<List<ShowModifierGroup>> GetModifierGroups()
    {
        List<ShowModifierGroup> list = new List<ShowModifierGroup>();
        var dbuser = _context.Modifiergroups.Where(a => a.Isdeleted == false).OrderBy(m=>m.Id).ToList();
        if (dbuser != null)
        {
            foreach (var modifiergroup in dbuser)
            {
                var (existingmodifier,temp) = GetModifierByModifierGroups(modifiergroup.Id,0,0,"");
                ShowModifierGroup c = new ShowModifierGroup()
                {
                    Id = modifiergroup.Id,
                    Name = modifiergroup.Modifiergroupname,
                    Description = modifiergroup.Modifiergroupname,
                    ExistingModifier = existingmodifier
                };
                list.Add(c);
            }
        }
        return list;
    }

    public (List<ShowModifier>,int) GetModifierByModifierGroups(int modifiergroupid,int page,int pageSize, string search)
    {
        List<ShowModifier> list = new List<ShowModifier>();
        int count=0;
        if(search==null) search="";
        var dbuser = _context.Modifiers.Include(m=>m.Modifiergroup).Where(a =>( a.Isdeleted == false && a.Modifiergroupid == modifiergroupid) && ( a.Modifiername.Contains(search) ));
        if(dbuser != null){

             count= dbuser.Count();
            dbuser = (pageSize != 0)?dbuser.Skip(page*pageSize ).Take(pageSize):dbuser;
            foreach (var modifier in dbuser)
            {
                ShowModifier c = new ShowModifier()
                {
                    Id = modifier.Id,
                    Name = modifier.Modifiername,
                    Unit = modifier.Unit,
                    Rate = (float)modifier.Modifierrate,
                    Quantity = modifier.Modifierquantity,
                };
                list.Add(c);
            }
        }
        return (list,count);
    }

}
