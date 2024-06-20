namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.CharactersF2;


/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F2 version.
/// </summary>
public class CharactersF2Controller : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
       
		return View(CharactersF2Repo.ListCharacters());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{  
		var sutCE = new CharactersCE();

		sutCE.Characters.CreationDate = DateTime.Now;
		
		PopulateLists(sutCE);

		return View(sutCE);
	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(int? save, int? add, int? remove, CharactersCE sutCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if( add != null )
		{
            
			//add entry for the new record
			var up =
				new CharactersCE.Character_Quest {
					 List_Id=
						sutCE.Quests.Count > 0 ?
						sutCE.Quests.Max(it => it.List_Id) + 1 :
						0,

					Description = null,
					Character_Id = 0,
					Quest_Id = 0,
				};
			sutCE.Quests.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();
            
			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if( remove != null )
		{
           
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			sutCE.Quests =
				sutCE
					.Quests
					.Where(it => it.List_Id != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//save of the form data was requested?
		if( save != null )
		{
           
			//form field validation passed?
			if( ModelState.IsValid )
			{Console.WriteLine("Cocka5");
				//create new 'Sutartis'
				sutCE.Characters.id = CharactersF2Repo.InsertCharacter(sutCE);
				//create new 'UzsakytosPaslaugos' records
				foreach ( var upVm in sutCE.Quests )
					CharactersF2Repo.InsertQuest(sutCE.Characters.id, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
                Console.WriteLine("Cocka6");
				PopulateLists(sutCE);
				return View(sutCE);
			}
		}
		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var sutCE = CharactersF2Repo.FindCharactersCE(id);
		
		sutCE.Quests = CharactersF2Repo.ListQuests(id);			
		PopulateLists(sutCE);

		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, int? save, int? add, int? remove, CharactersCE sutCE)
	{
		//addition of new 'UzsakytosPaslaugos' record was requested?
		if( add != null )
		{
			//add entry for the new record
			var up =
				new CharactersCE.Character_Quest {
					List_Id =
						sutCE.Quests.Count > 0 ?
						sutCE.Quests.Max(it => it.List_Id) + 1 :
						0,

					Description = null,
					Quest_Id = 0,
					Character_Id = 0,
					
				};
			sutCE.Quests.Add(up);

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//removal of existing 'UzsakytosPaslaugos' record was requested?
		if( remove != null )
		{
			//filter out 'UzsakytosPaslaugos' record having in-list-id the same as the given one
			sutCE.Quests =
				sutCE
					.Quests
					.Where(it => it.List_Id != remove.Value)
					.ToList();

			//make sure @Html helper is not reusing old model state containing the old list
			ModelState.Clear();

			//go back to the form
			PopulateLists(sutCE);
			return View(sutCE);
		}

		//save of the form data was requested?
		if( save != null )
		{
			//form field validation passed?
			if( ModelState.IsValid )
			{
				//update 'Sutartis'
				CharactersF2Repo.UpdateRegistracija(sutCE);

				//delete all old 'UzsakytosPaslaugos' records
				CharactersF2Repo.DeleteUzsakytaPaslaugaFromRegistracija(sutCE.Characters.id);

				//create new 'UzsakytosPaslaugos' records
				foreach( var upVm in sutCE.Quests )
					CharactersF2Repo.InsertQuest(sutCE.Characters.id, upVm);

				//save success, go back to the entity list
				return RedirectToAction("Index");
			}
			//form field validation failed, go back to the form
			else
			{
				PopulateLists(sutCE);
				return View(sutCE);
			}
		}

		//should not reach here
		throw new Exception("Should not reach here.");
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var sutCE = CharactersF2Repo.FindCharactersCE(id);
		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//load 'Sutartis'
		var sutCE = CharactersF2Repo.FindCharactersCE(id);
		if(sutCE == null) 
		{
			//delete the entity
			CharactersF2Repo.DeleteUzsakytaPaslaugaFromRegistracija(id);
			CharactersF2Repo.DeleteRegistracija(id);

			//redired to list form
			return RedirectToAction("Index");
		}
		else
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;
			return View("Delete", sutCE);
		}


	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="sutCE">'Sutartis' view model to append to.</param>
	private void PopulateLists(CharactersCE sutCE)
	{

		//load entities for the select lists
		var Users = UserRepo.List();
		var Faction = FactionRepo.List();
		var Class = ClassRepo.List();
        var Race_Info = Race_infoRepo.List();
        var Quests = QuestsRepo.List();

		sutCE.Lists.Users =
			Users
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.Username}"
                            
						};
				})
				.ToList();

		sutCE.Lists.Faction =
			Faction
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.Name}"
						};
				})
				.ToList();

		sutCE.Lists.Class =
			Class
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.Name}"
						};
				})
				.ToList();
        sutCE.Lists.Quests =
			Quests
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.Description}"
						};
				})
				.ToList();
		sutCE.Lists.Race_Info =
			Race_Info
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.id),
							Text = $"{it.Description}"
						};
				})
				.ToList();
	}
}
