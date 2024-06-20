namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Spells;


/// <summary>
/// Controller for working with 'Darbuotojas' entity.
/// </summary>
public class SpellsController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		//gražinamas darbuotoju sarašo vaizdas
		return View(SpellsRepo.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var spell = new SpellsCE();
		PopulateSelections(spell);
		return View(spell);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(SpellsCE spell)
	{
		var match = SpellsRepo.Find(spell.Spells.id);

		if (match != null)
			ModelState.AddModelError("id", "Field value already exists in database.");

		//form field validation passed?
		if (ModelState.IsValid)
		{
			//NOTE: insert will fail if someone managed to add different 'darbuotojas' with same 'tabelis' after the check
			SpellsRepo.Insert(spell);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(spell);
		return View(spell);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var spell = SpellsRepo.Find(id);
		PopulateSelections(spell);
		return View(spell);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, SpellsCE spell)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			SpellsRepo.Update(spell);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(spell);
		return View(spell);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var spell = SpellsRepo.FindForDeletion(id);
		return View(spell);
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
			SpellsRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch (MySql.Data.MySqlClient.MySqlException)
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var spell = SpellsRepo.FindForDeletion(id);
			return View("Delete", spell);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
	public void PopulateSelections(SpellsCE spell)
	{
		//load entities for the select lists
		var Class = ClassRepo.List();

		//build select lists
		spell.Lists.Class =
			Class.Select(it => {
				return
					new SelectListItem()
					{
						Value = Convert.ToString(it.id),
						Text = it.Name
					};
			})
			.ToList();
	}
}
