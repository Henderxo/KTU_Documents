namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Faction;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Faction{

    [DisplayName("ID")]
    [Required]
    public int id {get; set;}
    [DisplayName("Level")]
    [Required]
    public int Level{get; set;}
    [DisplayName("MemberSize")]
    [Required]
    public int MemberSize{get; set;}
     [DisplayName("Name")]
    [Required]
    public string Name{get; set;}
    [DisplayName("Owner")]
    [Required]
    public string Owner{get; set;}
}