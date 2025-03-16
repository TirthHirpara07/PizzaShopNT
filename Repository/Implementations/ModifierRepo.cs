using System.Security.Cryptography;
using Entity.Models;
using Entity.ViewModal;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class ModifierRepo : IModifierRepo
{
    private readonly PizzaShopContext _context;

    public ModifierRepo(PizzaShopContext context)
    {
        _context = context;
    }

    public async Task<List<ShowModifierGroup>> GetModifierGroups()
    {
        List<ShowModifierGroup> list = new List<ShowModifierGroup>();
        var dbuser = _context.Modifiergroups.Where(a => a.Isdeleted == false).OrderBy(m => m.Id).ToList();
        if (dbuser != null)
        {
            foreach (var modifiergroup in dbuser)
            {
                ShowModifierGroup c = new ShowModifierGroup()
                {
                    Id = modifiergroup.Id,
                    Name = modifiergroup.Modifiergroupname,
                    Description = modifiergroup.Modifiergroupdescription,
                };
                list.Add(c);
            }
        }
        return list;
    }

    public (List<ShowModifier>, int) GetModifierByModifierGroups(int modifiergroupid, int page, int pageSize, string search)
    {
        List<ShowModifier> list = new List<ShowModifier>();
        int count = 0;
        if (search == null) search = "";
        var dbuser = _context.Modifiers.Include(m => m.Modifiergroup).Where(a => (a.Isdeleted == false) && (a.Modifiername.Contains(search)));
        if (dbuser != null)
        {
            if (modifiergroupid != 0)
            {
                dbuser = dbuser.Where(m => m.Modifiergroupid == modifiergroupid);
            }
            count = dbuser.Count();
            dbuser = (pageSize != 0) ? dbuser.Skip(page * pageSize).Take(pageSize) : dbuser;
            foreach (var modifier in dbuser)
            {
                ShowModifier c = new ShowModifier()
                {
                    Id = modifier.Id,
                    Name = modifier.Modifiername,
                    ModifierGroupId = modifier.Modifiergroupid,
                    Unit = modifier.Unit,
                    Rate = (float)modifier.Modifierrate,
                    Quantity = modifier.Modifierquantity,
                    Description = modifier.Modifierdescription,
                };
                list.Add(c);
            }
        }
        return (list, count);
    }

    public async Task<bool> UpdateModifiersGroup(ShowModifierGroup bean)
    {
        try
        {


            var currentModifierGroup = _context.Modifiergroups.Where(a => a.Isdeleted == false && a.Id == bean.Id).FirstOrDefault();
            List<Modifier> existingModifiers = _context.Modifiers.Where(a => (a.Isdeleted == false) && (a.Modifiergroupid == bean.Id)).ToList();
            if (currentModifierGroup != null)
            {
                currentModifierGroup.Modifiergroupname = bean.Name;
                currentModifierGroup.Modifiergroupdescription = bean.Description;

                foreach (ShowModifier mod in bean.ExistingModifier)
                {
                    bool isPresent = existingModifiers.Any(m => m.Id == mod.Id);
                    if (!isPresent)
                    {
                        Modifier newmod = new Modifier()
                        {
                            Modifiername = mod.Name,
                            Modifiergroupid = bean.Id,
                            Modifierrate = mod.Rate,
                            Modifierdescription = mod.Description,
                            Unit = mod.Unit,
                            Modifierquantity = mod.Quantity,
                            Isdeleted = false,
                            Createddate = DateTime.Now,
                            Modifieddate = DateTime.Now,
                        };
                        _context.Modifiers.Add(newmod);
                    }
                }
                List<Modifier> deletedModifiers = existingModifiers.Where(existing => !bean.ExistingModifier.Any(mod => mod.Id == existing.Id)).ToList();
                if (deletedModifiers != null)
                {
                    foreach (Modifier mod in deletedModifiers)
                    {
                        mod.Isdeleted = true;
                    }
                }
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> DeleteModifierGroup(int id)
    {
        var dbuser = _context.Modifiers.Include(a => a.Modifiergroup).Where(a => a.Isdeleted == false && a.Modifiergroupid == id).ToList();
        if (dbuser != null)
        {
            foreach (var modifier in dbuser)
            {
                modifier.Isdeleted = true;
                modifier.Modifieddate = DateTime.Now;
                modifier.Modifiergroup.Isdeleted = true;
            }
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> AddnewModifier(ShowModifier bean)
    {
        try
        {


            var dbuser = _context.Modifiers.Where(m => m.Modifiername == bean.Name && m.Modifiergroupid == bean.ModifierGroupId).FirstOrDefault();
            if (dbuser == null)
            {
                Modifier modifier = new Modifier()
                {
                    Modifiername = bean.Name,
                    Modifiergroupid = bean.ModifierGroupId,
                    Unit = bean.Unit,
                    Modifierrate = bean.Rate,
                    Modifierquantity = bean.Quantity,
                    Modifierdescription = bean.Description,
                    Isdeleted = false,
                    Createddate = DateTime.Now,
                    Modifieddate = DateTime.Now
                };
                _context.Modifiers.Add(modifier);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ShowModifier> GetModifier(int id)
    {
        var dbuser = _context.Modifiers.Where(m => m.Id == id).FirstOrDefault();
        if (dbuser != null)
        {
            ShowModifier modifier = new ShowModifier()
            {
                Id = dbuser.Id,
                Name = dbuser.Modifiername,
                ModifierGroupId = dbuser.Modifiergroupid,
                Unit = dbuser.Unit,
                Quantity = dbuser.Modifierquantity,
                Rate = dbuser.Modifierrate,
                Description = dbuser.Modifierdescription
            };
            return modifier;
        }
        else
        {
            return new ShowModifier();
        }
    }

    public async Task<bool> UpdateModifier(ShowModifier bean)
    {
        var dbuser = _context.Modifiers.Where(m => m.Id == bean.Id).FirstOrDefault();
        if (dbuser != null)
        {
            dbuser.Modifiername = bean.Name;
            dbuser.Modifiergroupid = bean.ModifierGroupId;
            dbuser.Unit = bean.Unit;
            dbuser.Modifierquantity = bean.Quantity;
            dbuser.Modifierrate = bean.Rate;
            dbuser.Modifierdescription = bean.Description;
            dbuser.Modifieddate = DateTime.Now;

            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteModifier(List<ShowModifier> list)
    {
        if (list.Count > 0)
        {
            foreach (ShowModifier mod in list)
            {
                var dbuser = _context.Modifiers.Where(m=>m.Id == mod.Id).FirstOrDefault();
                if(dbuser != null)
                {
                    dbuser.Isdeleted = true;
                }
            }
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
