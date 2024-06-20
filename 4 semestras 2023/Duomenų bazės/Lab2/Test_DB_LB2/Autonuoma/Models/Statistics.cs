namespace Org.Ktu.Isk.P175B602.Autonuoma.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class Statistics{

    public int Attack_Points;
    public int Def_Points;
    public decimal Exp_ForLvlUp;
    public decimal Exp_Points;
    public int FkCharacter;

    public int FkPets;
    public int Gems;
    public string Gender;
    public int Gold;
    public decimal Health;
    public int id;
    public int Lvl;
    public decimal Mana;
    public int Movement_Speed;
}