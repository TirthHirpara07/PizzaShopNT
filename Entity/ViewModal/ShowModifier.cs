using System.ComponentModel.DataAnnotations;

namespace Entity.ViewModal;

public class ShowModifier
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Modifier name Required")]
    public string Name { get; set; }

    [Required(ErrorMessage ="Modifier Group name Required")]
    public int ModifierGroupId { get; set; }

    [Required(ErrorMessage ="Modifier Unit Required")]
    public string Unit { get; set; }

    [Required(ErrorMessage ="Modifier Rate Required")]
    public double Rate { get; set; }

    [Required(ErrorMessage ="Modifier Quantity Required")]
    public int Quantity { get; set; }

    public string Description { get; set;}

}
