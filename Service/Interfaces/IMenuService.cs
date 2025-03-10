
using Entity.Helper;
using Entity.ViewModal;

namespace Service.Interfaces;

public interface IMenuService
{
    public Task<RedirectResult> AddCategory(ShowCategory category);

    public Task<List<ShowCategory>> GetCategories();
    public Task<RedirectResult> UpdateCategory(ShowCategory category);
    public Task<RedirectResult> DeleteCategory(int id);
    public (List<ShowItem>,int) GetItemsByCategory(ItemPagination p);
    public Task<ItemPagination> UpdatePageDetails(ItemPagination p);
    public Task<List<ShowModifierGroup>> GetModifierGroups();
    public (List<ShowModifier>,int) GetModifierByModifierGroups(ItemPagination p);
}
