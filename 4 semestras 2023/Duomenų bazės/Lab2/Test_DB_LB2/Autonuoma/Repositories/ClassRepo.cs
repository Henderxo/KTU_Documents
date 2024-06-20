namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Class;



public class ClassRepo
{
	public static List<ClassL> List()
	{
		var query = $@"SELECT * FROM `class` ORDER BY id_Class ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<ClassL>(drc, (dre, t) => {
				t.id = dre.From<int>("id_Class");
				t.Difficulty = dre.From<string>("DifficultY");
				t.Description = dre.From<string>("Description");
				t.Name = dre.From<string>("Name");
                
			});

		return result;
	}

    public static ClassL Find(int id)
	{
		var query = $@"SELECT * FROM `class` WHERE id_Class=?id_Class";

		var drc =
			Sql.Query(query, args => {
				args.Add("?id_Class", id);
			});
			
		if( drc.Count > 0 )
		{
			var result =
			Sql.MapOne<ClassL>(drc, (dre, t) => {
				t.Description = dre.From<string>("Description");
				t.Difficulty = dre.From<string>("Difficulty");
				t.id = dre.From<int>("id_Class");			
				t.Name = dre.From<string>("Name");
				
			});
			
			return result;
		}
		return null;

	}


    

    public static void Update(ClassL classEvm)
	{
		var query =
			$@"UPDATE `class`
			SET
				Difficulty=?Difficulty,
                Description=?Description,
                Name=?Name
			WHERE
				id_Class=?id_Class";

		Sql.Update(query, args => {
			args.Add("?Difficulty", classEvm.Difficulty);
			args.Add("?Description", classEvm.Description);
			args.Add("?Name", classEvm.Name);
            args.Add("?id_Class", classEvm.id);
		});
	}


    public static void Insert(ClassL classEvm)
	{
		var query =
			$@"INSERT INTO `class`
			(
				Difficulty,
				Description,
                Name,
				id_Class
			)
			VALUES(
				?Difficulty,
				?Description,
                ?Name,
				?id_Class
			)";

		Sql.Insert(query, args => {
			args.Add("?Difficulty", classEvm.Difficulty);
			args.Add("?Description", classEvm.Description);
			args.Add("?Name", classEvm.Name);
			args.Add("?id_Class", classEvm.id);
		});
	}


    public static void DeleteClass(int id)
	{
		var query = $@"DELETE FROM `class` ORDER BY id_Class ASC";
		Sql.Delete(query, args => {
			args.Add("?id_Class", id);
		});
	}


}



