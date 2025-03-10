using Entity.ViewModal;

namespace Entity.Helper;

public class MenuServiceModel
{
    public ShowCategory Category { get; set; }
    public ShowItem Item { get; set; }
    public List<ShowCategory> CategoryList { get; set; }
    public List<ShowItem> ItemList { get; set; }
    public ShowModifierGroup ModifierGroup { get; set; }
    public ShowModifier Modifier { get; set; }
    public List<ShowModifierGroup> ModifierGroupList { get; set; }
    public List<ShowModifier> ModifierList { get; set; }

}
