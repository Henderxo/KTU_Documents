namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Race_info;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Race_info{
    [DisplayName("Ability")]
    [Required]
    public string Ability{get; set;}
    [DisplayName("BonusStats")]
    [Required]
    public string BonusStats{get; set;}
    [DisplayName("Description")]
    [Required]
    public string Description{get; set;}
    [DisplayName("id")]
    [Required]
    public int id {get; set;}
}