using Entity.Models;

namespace Entity.ViewModal;

public class ShowModifierGroup
{
      public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public List<ShowModifier> ExistingModifier { get; set; }
}
