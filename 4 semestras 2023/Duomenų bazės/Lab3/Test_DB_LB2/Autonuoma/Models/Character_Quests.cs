namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.Character_Quests;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


public class Character_Quests{
    [DisplayName("Character")]

    public int FkCharacter {get; set;}
    [DisplayName("Quest")]

    public int FkQuests {get; set;}
    [DisplayName("Id")]
    public int id {get; set;}
}


public class Character_QuestsL{
    [DisplayName("Character")]

    public int FkCharacter {get; set;}
    [DisplayName("Quest")]

    public int FkQuests {get; set;}
    [DisplayName("Id")]
    public int id {get; set;}
}

public class Character_QuestsCE
{
    public class Character_QuestsM
    {
        [DisplayName("Character")]
        [Required]
        public int FkCharacter {get; set;}
        [DisplayName("Quest")]
        [Required]
        public int FkQuests {get; set;}
        [DisplayName("Id")]
        public int id {get; set;}
    }

    public class ListsM{
        public IList<SelectListItem> Quests { get; set; }
        public IList<SelectListItem> Character { get; set; }
    }

    public ListsM Lists { get; set; } = new ListsM();

    public Character_QuestsM Quest { get; set; } = new Character_QuestsM();

}