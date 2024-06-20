namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Users;


public class UserRepo
{
	public static List<Users> List()
	{
		string query = $@"SELECT * FROM `users` ORDER BY id_User ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<Users>(drc, (dre, t) => {
				t.id = dre.From<int>("id_User");
				t.Email = dre.From<string>("Email");
                t.AccessType = dre.From<string>("AccessType");
                t.CreationDate = dre.From<DateTime>("CreationDate");
                t.Status = dre.From<string>("Status");
                t.Password = dre.From<string>("Password");
                t.Username = dre.From<string>("Username");
			});

		return result;
	}

	public static Users Find(int id)
	{
		var query = $@"SELECT * FROM `users` WHERE id_User=?id_User";
		var drc = 
			Sql.Query(query, args => {
				args.Add("?id_User", id);
			});
		if( drc.Count > 0 )
		{

			var result = 
			Sql.MapOne<Users>(drc, (dre, t) => {
				t.id = dre.From<int>("id_User");
				t.Email = dre.From<string>("Email");
                t.AccessType = dre.From<string>("AccessType");
                t.CreationDate = dre.From<DateTime>("CreationDate");
                t.Status = dre.From<string>("Status");
                t.Password = dre.From<string>("Password");
                t.Username = dre.From<string>("Username");
			});

		return result;
		}
		return null;
		
	}

	public static void Update(Users use)
	{			
		var query = 
			$@"UPDATE `users` 
			SET 
                Email=?Email,
                AccessType=?AccessType,
                CreationDate=?CreationDate,
                Status=?Status,
                Password=?Password,
                Username=?Username
			WHERE 
				id_User=?id_User";

		Sql.Update(query, args => {
			args.Add("?id_User", use.id);
			args.Add("?Email", use.Email);
            args.Add("?AccessType", use.AccessType);
            args.Add("?CreationDate", use.CreationDate);
            args.Add("?Status", use.Status);
            args.Add("?Password", use.Password);
            args.Add("?Username", use.Username);


		});							
	}

	public static void Insert(Users use)
	{			
		var query = $@"INSERT INTO `users` 
        (
            Email,
            AccessType,
            CreationDate,
            Status,
            Password,
            Username,
			id_User
        )
        VALUES(

            ?Email,
            ?AccessType,
            ?CreationDate,
            ?Status,
            ?Password,
            ?Username,
			?id_User

        )";
		Sql.Insert(query, args => {

			args.Add("?Email", use.Email);
            args.Add("?AccessType", use.AccessType);
            args.Add("?CreationDate", use.CreationDate);
            args.Add("?Status", use.Status);
            args.Add("?Password", use.Password);
            args.Add("?Username", use.Username);
			args.Add("?id_User", use.id);

		});
	}

	public static void Delete(int id)
	{			
		var query = $@"DELETE FROM `users` where id_User=?id_User";
		Sql.Delete(query, args => {
			args.Add("?id_User", id);
		});			
	}
}
