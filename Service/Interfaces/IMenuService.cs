
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
    public Task<string> UpdateModifiersGroup(ShowModifierGroup bean);
    public Task<RedirectResult> DeleteModifierGroup(int id);
    public Task<RedirectResult> AddnewModifier(ShowModifier category);
    public Task<ShowModifier> GetModifier(int id);
    public Task<RedirectResult> UpdateModifier(ShowModifier bean);
    public Task<RedirectResult> DeleteModifier(List<ShowModifier> list);

}
