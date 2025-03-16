using Entity.Helper;
using Entity.ViewModal;

namespace Repository.Interfaces;

public interface IItemRepo
{
    public Task<bool> AddCategory(ShowCategory category);
    public Task<List<ShowCategory>> GetCategories();
    public  Task<bool> UpdateCategory(ShowCategory category);
    public  Task<bool> DeleteCategory(int id);
    public (List<ShowItem>,int) GetItemsByCategory(int categoryid,int page,int pageSize,string search);
}
