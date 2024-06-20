namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Quests;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Quests{

    [DisplayName("Description")]
    [Required]
    public string Description {get; set;}

    [DisplayName("Exp")]
    [Required]
    public int Exp {get; set;}


    [DisplayName("Gold")]
    [Required]
    public int Gold {get; set;}

    [DisplayName("id")]
    [Required]
    public int id {get; set;}

    [DisplayName("Lvl_Req")]
    [Required]
    public int Lvl_Req {get; set;}
    [DisplayName("Status")]
    [Required]
    public string Status {get; set;}
}