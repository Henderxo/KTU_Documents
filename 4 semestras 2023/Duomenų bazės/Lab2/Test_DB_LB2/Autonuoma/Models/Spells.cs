namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Spells;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class SpellsL{

    [DisplayName("Damage")]
    [Required]
    public double Damage{ get; set; }
    [DisplayName("Description")]
    [Required]
    public string Description{ get; set; }
    [DisplayName("Lvl_Req")]
    [Required]
    public int Lvl_Req{ get; set; }
    [DisplayName("ManaCost")]
    [Required]
    public double ManaCost{ get; set; }
    [DisplayName("Target")]
    [Required]
    public string Target{ get; set; }
    [DisplayName("ClassName")]
    [Required]
    public string ClassName{ get; set; }
    [DisplayName("Id")]
    [Required]
    public int id{ get; set; }
}


public class SpellsCE
{
    public class SpellsM{

    [DisplayName("Damage")]
    [Required]
    public double Damage{ get; set; }
    [DisplayName("Description")]
    [Required]
    public string Description{ get; set; }
    [DisplayName("Id")]
    [Required]
    public int id{ get; set; }
    [DisplayName("Lvl_Req")]
    [Required]
    public int Lvl_Req{ get; set; }
    [DisplayName("ManaCost")]
    [Required]
    public double ManaCost{ get; set; }
    [DisplayName("Target")]
    [Required]
    public string Target{ get; set; }
    [DisplayName("ClassName")]
    [Required]
    public int FKClassName{ get; set; }
}


public class ListsM
	{
		public IList<SelectListItem> Class { get; set; }
	}

    /// <summary>
	/// Entity view.
	/// </summary>
	public SpellsM Spells { get; set; } = new SpellsM();

	/// <summary>
	/// Lists for drop down controls.
	/// </summary>
	public ListsM Lists { get; set; } = new ListsM();
}
