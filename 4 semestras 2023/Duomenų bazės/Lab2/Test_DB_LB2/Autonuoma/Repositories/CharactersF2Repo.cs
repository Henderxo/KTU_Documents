namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.CharactersF2;


/// <summary>
/// Database operations related to 'Sutartis' entity.
/// </summary>
public class CharactersF2Repo
{
	public static List<CharactersL> ListCharacters()
	{
		var query =
			$@"SELECT
				r.CreationDate,
				r.id,
				r.Name,
				r.Status,
				d.Name as Class,
                k.Username as Users,
                p.Description as Race_Info,
				z.Name as Faction
			FROM
				`characters` r
				LEFT JOIN `class` d ON r.fk_Classid_Class=d.id_Class
				LEFT JOIN `users` k ON r.fk_Userid_User=k.id_User
                LEFT JOIN `race_info` p ON r.fk_Race_Infoid_Race_Info=p.id_Race_Info
                LEFT JOIN `faction` z ON r.fk_Factionid_Faction=z.id_Faction
			ORDER BY r.id DESC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<CharactersL>(drc, (dre, t) => {
				t.CreationDate = dre.From<DateTime>("CreationDate");
				t.id = dre.From<int>("id");
				t.Name = dre.From<string>("Name");
				t.Status = dre.From<string>("Status");
				t.Class = dre.From<string>("Class");
				t.Users = dre.From<string>("Users");
                t.Race_Info = dre.From<string>("Race_Info");
                t.Faction = dre.From<string>("Faction");
			});

		return result;
	}

	public static CharactersCE FindCharactersCE(int id)
	{
		var query = $@"SELECT * FROM `characters` WHERE id=?id";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		var result =
			Sql.MapOne<CharactersCE>(drc, (dre, t) => {
				//make a shortcut
                
				var reg = t.Characters;

				//
                reg.Status = dre.From<string>("Status");
                reg.id = dre.From<int>("id");
				reg.CreationDate = dre.From<DateTime>("CreationDate");
				reg.BackStory = dre.From<string>("BackStory");
				reg.Name = dre.From<string>("Name");
				reg.FkUser = dre.From<int>("fk_Userid_User");
				reg.FkRace_info = dre.From<int>("fk_Race_Infoid_Race_Info");
				reg.FkFaction = dre.From<int>("fk_Factionid_Faction");
				reg.FkClass = dre.From<int>("fk_Classid_Class");
			});

		return result;
	}

	public static int InsertCharacter(CharactersCE regCE)
	{
		var query =
			$@"INSERT INTO `characters`
			(
				CreationDate,
				BackStory,
				id,
				Name,
				Status,
				fk_Userid_User,
				fk_Race_Infoid_Race_Info,
                fk_Factionid_Faction,
                fk_Classid_Class
			)
			VALUES(
				?CreationDate,
				?BackStory,
				?id,
				?Name,
				?Status,
				?fk_Userid_User,
				?fk_Race_Infoid_Race_Info,
                ?fk_Factionid_Faction,
                ?fk_Classid_Class
			)";

		var ID =
			Sql.Insert(query, args => {
				//make a shortcut
				var reg = regCE.Characters;

				//
				args.Add("?id", reg.id);
				args.Add("?CreationDate", reg.CreationDate);
				args.Add("?BackStory",reg.BackStory);
				args.Add("?id", reg.Name);
				args.Add("?Status", reg.Status);
				args.Add("?fk_Userid_User", reg.FkUser);
				args.Add("?fk_Race_Infoid_Race_Info", reg.FkRace_info);
                args.Add("?fk_Factionid_Faction", reg.FkFaction);
				args.Add("?fk_Classid_Class", reg.FkClass);
			});

		return (int)ID;
	}

	public static void UpdateRegistracija(CharactersCE regCE)
	{
		var query =
			$@"UPDATE `registracija`
			SET
				CreationDate = ?CreationDate,
				BackStory = ?BackStory,
				id = ?id,
				Name = ?Name,
				Status = ?Status,
				fk_Userid_User = ?fk_Userid_User,
				fk_Race_Infoid_Race_Info = ?fk_Race_Infoid_Race_Info,
                fk_Factionid_Faction = ?fk_Factionid_Faction,
                fk_Classid_Class = ?fk_Classid_Class
			WHERE id=?id";

		Sql.Update(query, args => {
			//make a shortcut
			var reg = regCE.Characters;

			//
			args.Add("?id", reg.id);
				args.Add("?CreationDate", reg.CreationDate);
				args.Add("?BackStory",reg.BackStory);
				args.Add("?id", reg.Name);
				args.Add("?Status", reg.Status);
				args.Add("?fk_Userid_User", reg.FkUser);
				args.Add("?fk_Race_Infoid_Race_Info", reg.FkRace_info);
                args.Add("?fk_Factionid_Faction", reg.FkFaction);
				args.Add("?fk_Classid_Class", reg.FkClass);
		});
	}

	public static void DeleteRegistracija(int id)
	{
		var query = $@"DELETE FROM `characters` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});
	}


	public static List<CharactersCE.Character_Quest> ListQuests(int Character_id)
	{
		var query =
			$@"SELECT pas.Description AS Description

			FROM `character_quests` q
			LEFT JOIN `quests` pas ON pas.id=q.fk_Questsid
			WHERE fk_Characterid = ?Character_id
			ORDER BY id_Quest ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?Character_id", Character_id);
                
			});

		var result =
			Sql.MapAll<CharactersCE.Character_Quest>(drc, (dre, t) => {
				t.Description = dre.From<string>("Description");
			});

		for( int i = 0; i < result.Count; i++ )
			result[i].List_Id = i;

		return result;
	}

	public static void InsertQuest(int registracijosId, CharactersCE.Character_Quest up)
	{

		var query =
			$@"INSERT INTO `character_quests`
				(
					fk_Characterid,
					fk_Questsid,
					id_Quest

					
				)
				VALUES(
					?fk_Characterid,
					?fk_Questsid,
					?id_Quest
				)";

		Sql.Insert(query, args => {
			args.Add("?fk_Characterid", registracijosId);
			args.Add("?fk_Questsid", Convert.ToInt32(up.Quest_Id));
            args.Add("?id_Quest", up.List_Id);
		});
	}

	public static void DeleteUzsakytaPaslaugaFromRegistracija(int registracija)
	{
		var query =
			$@"DELETE FROM u
			USING `character_quests` AS u
			WHERE u.fk_Characterid=?fkid";

		Sql.Delete(query, args => {
			args.Add("?fkid", registracija);
		});
	}
}