using Entity.ViewModal;

namespace Repository.Interfaces;

public interface IModifierRepo
{
    public Task<List<ShowModifierGroup>> GetModifierGroups();
    public (List<ShowModifier>,int) GetModifierByModifierGroups(int modifiergroupid,int page,int pageSize, string search);
    public Task<bool> UpdateModifiersGroup(ShowModifierGroup bean);
    public Task<bool> DeleteModifierGroup(int id);
    public Task<bool> AddnewModifier(ShowModifier category);
    public Task<ShowModifier> GetModifier(int id);
    public Task<bool> UpdateModifier(ShowModifier bean);
    public Task<bool> DeleteModifier(List<ShowModifier> list);
}
