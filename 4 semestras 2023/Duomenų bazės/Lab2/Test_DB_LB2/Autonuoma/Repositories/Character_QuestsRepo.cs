/*namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Character_Quests;


public class Charachter_QuestsRepo
{
	public static List<Character_Quests> List()
	{
		var query =
			$@"SELECT
				md.id_Quest,
				md.fk_Questsid AS Quest,
                md.fk_Characterid AS Character
			FROM
				`Character_quests` md
				LEFT JOIN `Character_quests` a ON a.fk_Questsid =md.fk_marke
                LEFT JOIN `Character_quests` b ON b.fk_Questsid =md.fk_marke
			ORDER BY md.id_Quests ASC";

		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Character_Quests>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Marke = dre.From<string>("marke");
			});

		return result;
	}

	public static List<Character_Quests> ListForMarke(int markeId)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}modeliai` WHERE fk_marke=?markeId ORDER BY id ASC";
		var drc =
			Sql.Query(query, args => {
				args.Add("?markeId", markeId);
			});

		var result = 
			Sql.MapAll<Character_Quests>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.FkMarke = dre.From<int>("fk_marke");
			});

		return result;
	}

	public static Character_QuestsCE Find(int id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}modeliai` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Character_QuestsCE>(drc, (dre, t) => {
				t.Model.Id = dre.From<int>("id");
				t.Model.Pavadinimas = dre.From<string>("pavadinimas");
				t.Model.FkMarke = dre.From<int>("fk_marke");
			});

		return result;
	}

	public static Character_QuestsL FindForDeletion(int id)
	{
		var query =
			$@"SELECT
				md.id,
				md.pavadinimas,
				mark.pavadinimas AS marke
			FROM
				`{Config.TblPrefix}modeliai` md
				LEFT JOIN `{Config.TblPrefix}markes` mark ON mark.id=md.fk_marke
			WHERE
				md.id = ?id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result = 
			Sql.MapOne<Character_QuestsL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("pavadinimas");
				t.Marke = dre.From<string>("marke");
			});

		return result;
	}

	public static void Update(Character_QuestsCE modelisEvm)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}modeliai`
			SET
				pavadinimas=?pavadinimas,
				fk_marke=?marke
			WHERE
				id=?id";

		Sql.Update(query, args => {
			args.Add("?pavadinimas", modelisEvm.Model.Pavadinimas);
			args.Add("?marke", modelisEvm.Model.FkMarke);
			args.Add("?id", modelisEvm.Model.Id);
		});
	}

	public static void Insert(Character_QuestsCE modelisEvm)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}modeliai`
			(
				pavadinimas,
				fk_marke
			)
			VALUES(
				?pavadinimas,
				?marke
			)";

		Sql.Insert(query, args => {
			args.Add("?pavadinimas", modelisEvm.Model.Pavadinimas);
			args.Add("?marke", modelisEvm.Model.FkMarke);
		});
	}

	public static void Delete(int id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}modeliai` WHERE id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}
}
*/