namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Race_info;


public class Race_infoRepo
{
	public static List<Race_info> List()
	{
		string query = $@"SELECT * FROM `race_info` ORDER BY id_Race_Info ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Race_info>(drc, (dre, t) => {
				t.id = dre.From<int>("id_Race_Info");
                t.BonusStats = dre.From<string>("BonusStats");
				t.Description = dre.From<string>("Description");
                t.Ability = dre.From<string>("Ability");
			});

		return result;
	}

	public static Race_info Find(int id)
	{
		var query = $@"SELECT * FROM `race_info` WHERE id_Race_Info=?id_Race_Info";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id_Race_Info", id);
			});
		if( drc.Count > 0 )
		{
			var result = 
			Sql.MapOne<Race_info>(drc, (dre, t) => {
				t.id = dre.From<int>("id_Race_Info");
                t.BonusStats = dre.From<string>("BonusStats");
				t.Description = dre.From<string>("Description");
                t.Ability = dre.From<string>("Ability");
			});

		return result;
		}
		return null;
	}

	public static void Update(Race_info use)
	{			
		var query = 
			$@"UPDATE `race_info` 
			SET 
                BonusStats=?BonusStats,
                Ability=?Ability,
                Description=?Description

			WHERE 
                id_Race_Info=?id_Race_Info";

		Sql.Update(query, args => {
			args.Add("?id_Race_Info", use.id);
			args.Add("?Description", use.Description);
            args.Add("?Ability", use.Ability);
            args.Add("?BonusStats", use.BonusStats);

		});							
	}

	public static void Insert(Race_info use)
	{			
		var query = $@"INSERT INTO `race_info` 
        (
            Description,
            Ability,
            BonusStats,
			id_Race_Info
        )
        VALUES(

            ?Description,
            ?Ability,
            ?BonusStats,
			?id_Race_Info


        )";
		Sql.Insert(query, args => {

			args.Add("?id_Race_Info", use.id);
			args.Add("?Description", use.Description);
            args.Add("?Ability", use.Ability);
            args.Add("?BonusStats", use.BonusStats);

		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `race_info` where id_Race_Info=?id_Race_Info";
		Sql.Delete(query, args => {
			args.Add("?id_Race_Info", id);
		});			
	}
}
