namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using CharacterGold = Org.Ktu.Isk.P175B602.Autonuoma.Models.CharacterQuests;



/// <summary>
/// Controller for producing reports.
/// </summary>
public class CharacterQuestsController : Controller
{
	

	/// <summary>
	/// Produces contracts report.
	/// </summary>
	/// <param name="dateFrom">Starting date. Can be null.</param>
	/// <param name="dateTo">Ending date. Can be null.</param>
	/// <returns>Report view.</returns>
	[HttpGet]
	public ActionResult GoldSum(DateTime? dateFrom, DateTime? dateTo)
	{
		var report = new CharacterGold.Report();
        report.DateFrom = dateFrom;
		report.DateTo = dateTo?.AddHours(23).AddMinutes(59).AddSeconds(59);

		report.Quests = CharacterQuestsRepo.GetCharacters(report.DateFrom, report.DateTo);

		foreach (var item in report.Quests)
		{
            report.GoldSum += item.Gold;
		}

		return View(report);
	}

	
}