namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.CharactersF2;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;




public class CharactersL{
  
    [DisplayName("Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [Required]
    public DateTime CreationDate {get; set;}
    
    [DisplayName("Id")]
    [Required]
    public int id {get; set;}

    [DisplayName("Name")]
    [Required]
    public string Name {get; set;}

    [DisplayName("Status")]
    [Required]
    public string Status {get; set;}

    [DisplayName("User")]
    [Required]
    public string Users {get; set;}

    [DisplayName("Faction")]
    [Required]
    public string Faction {get; set;}

    [DisplayName("Race_Info")]
    [Required]
    public string Race_Info {get; set;}

    [DisplayName("Class")]
    [Required]
    public string Class {get; set;}
}


public class CharactersCE
{

    public class CharactersM
    {
        
        [DisplayName("BackStory")]
        [Required]
        public string BackStory{get; set;}

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime CreationDate{get; set;}  

        [DisplayName("FkFaction")]
        [Required]
        public int FkFaction {get; set;}

        [DisplayName("FkRace_Info")]
        [Required]
        public int FkRace_info {get; set;}

        [DisplayName("FkUser")]
        [Required]
        public int FkUser {get; set;}
        
        [DisplayName("FkClass")]
        [Required]
        public int FkClass{get; set;}

        [DisplayName("Id")]
        [Required]
        public int id {get; set;}

        [DisplayName("Name")]
        [Required]
        public string Name {get; set;}

        [DisplayName("Status")]
        [Required]
        public string Status {get; set;}
    
    }


    public class Character_Quest{
        [DisplayName("Quest_Id")]
        [Required]
        public int Quest_Id {get; set;}
        [DisplayName("Description")]
        [Required]
        public string Description {get; set;}
        [DisplayName("List_Id")]
        [Required]
         public int List_Id {get; set;}
        [DisplayName("Character_Id")]
        [Required]
         public int Character_Id {get; set;}
        
        
    }

    public class ListsM
	{
		public IList<SelectListItem> Users { get; set; }
		public IList<SelectListItem> Faction { get; set; }
		public IList<SelectListItem> Class { get; set; }
		public IList<SelectListItem> Race_Info { get; set; }
        public IList<SelectListItem> Quests { get; set; }

	}
    public CharactersM Characters {get; set;} = new CharactersM();

    public ListsM Lists { get; set; } = new ListsM();

    public IList<Character_Quest> Quests {get; set;} = new List<Character_Quest>();

}