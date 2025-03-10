using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.ViewModal;

public class ShowItem
{
    public int Id { get; set; }
    public string ItemImg { get; set; }
    public string Name { get; set; }
        public ItemType ItemType { get; set; }
    public float Rate { get; set; }
    public int Quantity { get; set; }
    public bool Available { get; set; }

}
public enum ItemType{
    Veg,
    NonVeg
}