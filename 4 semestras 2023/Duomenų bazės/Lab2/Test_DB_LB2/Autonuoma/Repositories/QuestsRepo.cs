namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Quests;


public class QuestsRepo
{
	public static List<Quests> List()
	{
		string query = $@"SELECT * FROM `quests` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Quests>(drc, (dre, t) => {
				t.id = dre.From<int>("id");
				t.Exp = dre.From<int>("Exp");
                t.Description = dre.From<string>("Description");
                t.Gold = dre.From<int>("Gold");
                t.Lvl_Req = dre.From<int>("Lvl_Req");
                t.Status = dre.From<string>("Status");
			});

		return result;
	}

	public static Quests Find(int id)
	{
		var query = $@"SELECT * FROM `quests` WHERE id=?id";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id", id);
			});

		if( drc.Count > 0 )
		{
			var result = 
			Sql.MapOne<Quests>(drc, (dre, t) => {
				t.id = dre.From<int>("id");
				t.Exp = dre.From<int>("Exp");
                t.Description = dre.From<string>("Description");
                t.Gold = dre.From<int>("Gold");
                t.Lvl_Req = dre.From<int>("Lvl_Req");
                t.Status = dre.From<string>("Status");
			});

		return result;
		}
		return  null;

		
	}

	public static void Update(Quests use)
	{			
		var query = 
			$@"UPDATE `quests` 
			SET 
                Description=?Description,
                Exp=?Exp,
                Gold=?Gold,
                Lvl_Req=?Lvl_Req,
                Status=?Status
			WHERE 
                id=?id";

		Sql.Update(query, args => {
			args.Add("?id", use.id);
			args.Add("?Description", use.Description);
            args.Add("?Exp", use.Exp);
            args.Add("?Gold", use.Gold);
            args.Add("?Lvl_Req", use.Lvl_Req);
            args.Add("?Status", use.Status);

		});							
	}

	public static void Insert(Quests use)
	{			
		var query = $@"INSERT INTO `quests` 
        (
            Description,
            Exp,
            Gold,
            Lvl_Req,
            Status,
			id
        )
        VALUES(

            ?Description,
            ?Exp,
            ?Gold,
            ?Lvl_Req,
            ?Status,
			?id


        )";
		Sql.Insert(query, args => {

			args.Add("?Description", use.Description);
            args.Add("?Exp", use.Exp);
            args.Add("?Gold", use.Gold);
            args.Add("?Lvl_Req", use.Lvl_Req);
            args.Add("?Status", use.Status);
			args.Add("?id", use.id);

		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `quests` where id=?id";
		Sql.Delete(query, args => {
			args.Add("?id", id);
		});			
	}
}
