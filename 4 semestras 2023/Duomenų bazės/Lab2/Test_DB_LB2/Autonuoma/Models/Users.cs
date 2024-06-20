namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Users;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Users{
    [DisplayName("AccessType")]
    [Required]
    public string AccessType {get; set;}
    [DisplayName("CreationDate")]  
    [DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [Required]
    public DateTime? CreationDate {get; set;}
    [DisplayName("Email")]
    [EmailAddress]
    [Required]
    public string Email {get; set;}
    [DisplayName("Id")]
    [Required]
    public int id {get; set;}
    [DisplayName("Password")]
    [PasswordPropertyText]
    [Required]
    public string Password {get; set;}
    [DisplayName("Status")]
    [Required]
    public string Status {get; set;}
    [DisplayName("Username")]
    [Required]
    public string Username {get; set;}
}