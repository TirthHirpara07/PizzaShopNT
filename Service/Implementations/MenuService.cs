using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Entity.Helper;
using Entity.ViewModal;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class MenuService : IMenuService
{
    private readonly INotyfService _notyf;
    private readonly IItemRepo _itemrepo;
    private readonly IModifierRepo _modifierrepo;
    
    
    public MenuService(IItemRepo itemrepo, INotyfService notyf,IModifierRepo modifierrepo){
        _itemrepo = itemrepo;
        _notyf = notyf;
        _modifierrepo=modifierrepo;
    }



    public async Task<List<ShowCategory>> GetCategories()
    {
        List<ShowCategory> list =await _itemrepo.GetCategories();
        return list;
    }
    
    public async Task<RedirectResult> AddCategory(ShowCategory category)
    {
        bool isadded =await _itemrepo.AddCategory(category);
        if(isadded){
           _notyf.Success("Category Added Successfully");
        }
        else _notyf.Error("Category didn't Added ");
        return  new RedirectResult { shouldRedirect = true, controller = "Menu", action = "index" };

    }

    public async Task<RedirectResult> UpdateCategory(ShowCategory category)
    {
         bool isupdated =await _itemrepo.UpdateCategory(category);
        if(isupdated){
           _notyf.Success("Category Updated Successfully");
        }
        else _notyf.Error("Category didn't Updated");
        return  new RedirectResult { shouldRedirect = true, controller = "Menu", action = "index" };
    }

    public async Task<RedirectResult> DeleteCategory(int id)
    {
       bool isdeleted =await _itemrepo.DeleteCategory(id);
        if(isdeleted){
           _notyf.Success("Category Deleted Successfully");
        }
        else _notyf.Error("Category didn't Deleted");
        return  new RedirectResult { shouldRedirect = true, controller = "Menu", action = "index" };
    }

    public (List<ShowItem>,int) GetItemsByCategory(ItemPagination p)
    {
        var (list,count) = _itemrepo.GetItemsByCategory(p.Category,p.page,p.pageSize,p.search);
        return(list,count);
    }

    public async Task<ItemPagination> UpdatePageDetails(ItemPagination p)
    {
        p.maxPage = (int)Math.Ceiling(((double)p.count / p.pageSize));
        p.start = p.page * p.pageSize + 1;
        p.end = (((p.page * p.pageSize) + p.pageSize)>=p.count)? p.count :(p.page * p.pageSize) + p.pageSize;
        return p;
    }

    public async Task<List<ShowModifierGroup>> GetModifierGroups()
    {
        List<ShowModifierGroup> list =await _modifierrepo.GetModifierGroups();
        return list;
    }

    public (List<ShowModifier>,int) GetModifierByModifierGroups(ItemPagination p)
    {
        var (list,count) = _modifierrepo.GetModifierByModifierGroups(p.Category,p.page,p.pageSize,p.search);
        return(list,count);
    }
}
