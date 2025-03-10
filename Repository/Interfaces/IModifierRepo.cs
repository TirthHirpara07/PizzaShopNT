using Entity.ViewModal;

namespace Repository.Interfaces;

public interface IModifierRepo
{
    public Task<List<ShowModifierGroup>> GetModifierGroups();
    public (List<ShowModifier>,int) GetModifierByModifierGroups(int modifiergroupid,int page,int pageSize, string search);
}
