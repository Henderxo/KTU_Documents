namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Faction;


/// <summary>
/// Controller for working with 'Automobilis' entity.
/// </summary>
public class FactionController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(FactionRepo.List());
	}


    [HttpGet]
	public ActionResult Create()
	{
		var modelisEvm = new Faction();

		return View(modelisEvm);
	}

    /// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var ClassEvm = FactionRepo.Find(id);
		return View(ClassEvm);
	}

    [HttpPost]
	public ActionResult Create(Faction klientas)
	{
		//do not allow creation of entity with 'asmensKodas' field matching existing one
		var match = FactionRepo.Find(klientas.id);

		if( match !=null )
			ModelState.AddModelError("id_User", "Field value already exists in database.");

		//form field validation passed?
		if( ModelState.IsValid )
		{
			FactionRepo.Insert(klientas);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		
		//form field validation failed, go back to the form
		return View(klientas);
	}


	[HttpGet]
	public ActionResult Edit(int id)
	{
		return View(FactionRepo.Find(id));
	}

    [HttpPost]
	public ActionResult Edit(int Asmens_kodas, Faction klientas)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			FactionRepo.Update(klientas);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		return View(klientas);
	}

    /// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try 
		{
			FactionRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var ClassEvm = FactionRepo.Find(id);

			return View("Delete", ClassEvm);
		}
	}



	
}
