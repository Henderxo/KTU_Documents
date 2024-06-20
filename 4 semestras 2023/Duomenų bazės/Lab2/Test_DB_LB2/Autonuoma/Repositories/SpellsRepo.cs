namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Spells;


public class SpellsRepo
{
	public static List<SpellsL> List()
	{
		var query =
			$@"SELECT
				cp.Damage,
				cp.Description,
				cp.Lvl_Req,
				cp.ManaCost,
				cp.id_Spells,
				cp.Target,
				ko.Name AS ClassName
			FROM
				`spells` cp
				LEFT JOIN `class` ko ON ko.id_Class=cp.fk_Classid_Class
			ORDER BY cp.id_Spells ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<SpellsL>(drc, (dre, t) => {
				t.Damage = dre.From<double>("Damage");
                t.Description = dre.From<string>("Description");
                t.Lvl_Req = dre.From<int>("Lvl_Req");
                t.ManaCost = dre.From<double>("ManaCost");
				t.id = dre.From<int>("id_Spells");
                t.Target = dre.From<string>("Target");
				t.ClassName = dre.From<string>("ClassName");
			});

		return result;
	}

	public static SpellsCE Find(int id)
	{
		var query = $@"SELECT * FROM `spells` WHERE id_Spells=?id_Spells";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id_Spells", id);
			});
		
		if( drc.Count > 0 )
		{
			var result = 
			Sql.MapOne<SpellsCE>(drc, (dre, t) => {
				t.Spells.id = dre.From<int>("id_Spells");
				t.Spells.Damage = dre.From<double>("Damage");
                t.Spells.Description = dre.From<string>("Description");
                t.Spells.Lvl_Req = dre.From<int>("Lvl_Req");
                t.Spells.ManaCost = dre.From<double>("ManaCost");
                t.Spells.Target = dre.From<string>("Target");
				t.Spells.FKClassName = dre.From<int>("fk_Classid_Class");
			});

		return result;
		}
		return null;
		
	}



	public static SpellsL FindForDeletion(int id)
	{
		var query =
			$@"SELECT
				db.Damage,
				db.Description,
				db.Lvl_Req,
				db.ManaCost,
				db.id_Spells,
				db.Target,
				kl.Name AS ClassName
			FROM
				`spells` db
				LEFT JOIN `class` kl ON kl.id_Class=db.fk_Classid_Class
			WHERE
				db.id_Spells = ?id_Spells";
		var drc =
			Sql.Query(query, args => {
				args.Add("?id_Spells", id);
			});

		var result =
			Sql.MapOne<SpellsL>(drc, (dre, t) => {
				t.Damage = dre.From<double>("Damage");
                t.Description = dre.From<string>("Description");
                t.Lvl_Req = dre.From<int>("Lvl_Req");
                t.ManaCost = dre.From<double>("ManaCost");
				t.id = dre.From<int>("id_Spells");
                t.Target = dre.From<string>("Target");
				t.ClassName = dre.From<string>("ClassName");
			});

		return result;
	}

	public static void Update(SpellsCE spell)
	{			
		var query = 
			$@"UPDATE `spells` 
			SET 
				Damage = ?Damage,
                Description = ?Description,
                Lvl_Req = ?Lvl_Req,
                fk_Classid_Class = ?fk_Classid_Class,
                ManaCost = ?ManaCost,
            	Target = ?Target
			WHERE 
				id_Spells=?id_Spells";

		Sql.Update(query, args => {
			args.Add("?Damage", spell.Spells.Damage);
            args.Add("?Description", spell.Spells.Description);
            args.Add("?Lvl_Req", spell.Spells.Lvl_Req);
            args.Add("?fk_Classid_Class", spell.Spells.FKClassName);
            args.Add("?ManaCost", spell.Spells.ManaCost);
            args.Add("?Target", spell.Spells.Target);
			args.Add("?id_Spells", spell.Spells.id);
		});							
	}

	public static void Insert(SpellsCE spell)
	{			
		var query = $@"INSERT INTO `spells` 
        (
            Damage,
            Description,
            Lvl_Req,
            fk_Classid_Class,
            ManaCost,
			id_Spells, 
            Target

        )
        VALUES(

            ?Damage,
            ?Description,
            ?Lvl_Req,
            ?fk_Classid_Class,
            ?ManaCost, 
			?id_Spells,
            ?Target

        )";
		Sql.Insert(query, args => {
			args.Add("?Damage", spell.Spells.Damage);
            args.Add("?Description", spell.Spells.Damage);
            args.Add("?Lvl_Req", spell.Spells.Lvl_Req);
            args.Add("?fk_Classid_Class", spell.Spells.FKClassName);
            args.Add("?ManaCost", spell.Spells.ManaCost);
			args.Add("?id_Spells", spell.Spells.id);
            args.Add("?Target", spell.Spells.Target);

		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `spells` where id_Spells=?id_Spells";
		Sql.Delete(query, args => {
			args.Add("?id_Spells", id);
		});			
	}
}
