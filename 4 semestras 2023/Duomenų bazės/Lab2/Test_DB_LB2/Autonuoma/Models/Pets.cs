namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Pets{

    public int Age;

    public string Description;

    public int FkCharacter;

    public int id;

    public string Name;

    public string Status;

    public string Type;
}