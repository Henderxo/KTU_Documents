namespace Org.Ktu.Isk.P175B602.Autonuoma.Models.CharacterQuests;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// View model for single contract in a report.
/// </summary>
public class CharacterQuests
{
	[DisplayName("UserId")]
	public int UserId { get; set; }

    [DisplayName("CharacterID")]
	public int CharacterID { get; set; }

    [DisplayName("Username")]
    public string Username { get; set; }

	[DisplayName("CreationDate")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime CreationDate { get; set; }

    [DisplayName("CharacterName")]
	public string CharacterName { get; set; }

    [DisplayName("Gold")]
	public int  Gold { get; set; }

	public int  AllGold { get; set; }
	
}

/// <summary>
/// View model for whole report.
/// </summary>
public class Report
{

    [DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateFrom { get; set; }

	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
	public DateTime? DateTo { get; set; }

	public int GoldSum { get; set; }

    public List<CharacterQuests> Quests { get; set; }
}