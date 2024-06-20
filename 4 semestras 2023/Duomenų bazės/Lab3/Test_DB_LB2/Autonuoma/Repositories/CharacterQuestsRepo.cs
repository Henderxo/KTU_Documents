namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using CharacterGold = Org.Ktu.Isk.P175B602.Autonuoma.Models.CharacterQuests;


/// <summary>
/// Database operations related to reports.
/// </summary>
public class CharacterQuestsRepo
{
	

	public static List<CharacterGold.CharacterQuests> GetCharacters(DateTime? dateFrom, DateTime? dateTo)
	{
		var query =
			$@"SELECT
    cha.Name,
    cha.id,
    usr.Username,
    cha.CreationDate,
    usr.id_User,
    sta.Gold,
    bs1.AllGold
FROM
    `characters` cha
    INNER JOIN `users` usr ON usr.id_User = cha.fk_Userid_User
    LEFT JOIN `statistics` sta ON sta.fk_Characterid = cha.id
    LEFT JOIN
        (
            SELECT 
                cha2.fk_Userid_User,
                sum(sta2.Gold) as AllGold
            FROM `characters` cha2
            INNER JOIN `statistics` sta2 ON sta2.fk_Characterid = cha2.id
            WHERE
                cha2.CreationDate >= IFNULL(?nuo, cha2.CreationDate)
                AND cha2.CreationDate <= IFNULL(?iki, cha2.CreationDate)
            GROUP BY cha2.fk_Userid_User
        ) AS bs1
        ON bs1.fk_Userid_User = cha.fk_Userid_User
WHERE
    cha.CreationDate >= IFNULL(?nuo, cha.CreationDate)
    AND cha.CreationDate <= IFNULL(?iki, cha.CreationDate)
GROUP BY 
    usr.id_User, cha.id
ORDER BY 
    usr.Username ASC";

		var drc =
			Sql.Query(query, args => {
				args.Add("?nuo", dateFrom);
				args.Add("?iki", dateTo);
			});

		var result = 
			Sql.MapAll<CharacterGold.CharacterQuests>(drc, (dre, t) => {
				t.UserId = dre.From<int>("id_User");
				t.Username = dre.From<string>("Username");
				t.CharacterID = dre.From<int>("id");
				t.CharacterName = dre.From<string>("Name");
				t.CreationDate = dre.From<DateTime>("CreationDate");
				t.Gold = dre.From<int>("Gold");
				t.AllGold = dre.From<int>("AllGold");
			});

		return result;
	}


    

	
}
