namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Class;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class ClassL{

    [DisplayName("Description")]
    [Required]
    public string Description { get; set;}

    [DisplayName("Difficulty")]
    [Required]
    public string Difficulty { get; set;}

    [DisplayName("Id")]
    [Required]
    public int id { get; set;}

    [DisplayName("Name")]
    [Required]
    public string Name { get; set;}
}






