namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Faction;


public class FactionRepo
{
	public static List<Faction> List()
	{
		string query = $@"SELECT * FROM `faction` ORDER BY id_Faction ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Faction>(drc, (dre, t) => {
				t.id = dre.From<int>("id_Faction");
				t.Level = dre.From<int>("Level");
                t.MemberSize = dre.From<int>("MemberSize");
                t.Name = dre.From<string>("Name");
                t.Owner = dre.From<string>("Owner");
			});

		return result;
	}

	public static Faction Find(int id)
	{
		var query = $@"SELECT * FROM `faction` WHERE id_Faction=?id_Faction";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id_Faction", id);
			});
		if( drc.Count > 0 )
		{
			var result = 
			Sql.MapOne<Faction>(drc, (dre, t) => {
				t.id = dre.From<int>("id_Faction");
				t.Level = dre.From<int>("Level");
                t.MemberSize = dre.From<int>("MemberSize");
                t.Name = dre.From<string>("Name");
                t.Owner = dre.From<string>("Owner");
			});

		return result;
		}
		return null;
		
	}

	public static void Update(Faction fac)
	{			
		var query = 
			$@"UPDATE `faction` 
			SET 
                Level=?Level,
                MemberSize=?MemberSize,
                Name=?Name,
                Owner=?Owner
			WHERE 
				id_Faction=?id_Faction";

		Sql.Update(query, args => {
			args.Add("?id_Faction", fac.id);
			args.Add("?Level", fac.Level);
            args.Add("?MemberSize", fac.MemberSize);
            args.Add("?Name", fac.Name);
            args.Add("?Owner", fac.Owner);

		});							
	}

	public static void Insert(Faction fac)
	{			
		var query = $@"INSERT INTO `Faction` 
        (
            Level,
            MemberSize,
            Name,
            Owner,
			id_Faction


        )
        VALUES(

            ?Level,
            ?MemberSize,
            ?Name,
            ?Owner,
			?id_Faction

        )";
		Sql.Insert(query, args => {
			args.Add("?Level", fac.Level);
            args.Add("?MemberSize", fac.MemberSize);
            args.Add("?Name", fac.Name);
            args.Add("?Owner", fac.Owner);
			args.Add("?id_Faction", fac.id);
		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `faction` where id_Faction=?id_Faction";
		Sql.Delete(query, args => {
			args.Add("?id_Faction", id);
		});			
	}
}
