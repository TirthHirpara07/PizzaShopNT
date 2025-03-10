using Entity.Helper;
using Entity.ViewModal;

namespace Repository.Interfaces;

public interface IItemRepo
{
    Task<bool> AddCategory(ShowCategory category);
    public Task<List<ShowCategory>> GetCategories();
    Task<bool> UpdateCategory(ShowCategory category);
    Task<bool> DeleteCategory(int id);
    public (List<ShowItem>,int) GetItemsByCategory(int categoryid,int page,int pageSize,string search);
}
