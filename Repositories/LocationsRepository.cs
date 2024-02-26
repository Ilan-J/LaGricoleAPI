using LaGricoleAPI.Models;
using LaGricoleAPI.DataBase;
using LaGricoleAPI.Utils.Mappers;
using MySqlConnector;

namespace LaGricoleAPI.Repositories;

sealed class LocationsRepository
{
	private const string TABLE_NAME	= "location";
	private const string FIELD_PK	= "pk_location";
	private const string FIELD_CITY	= "city";

	private const string SLTALL = $"SELECT * FROM	`{TABLE_NAME}`;";
	private const string SELECT = $"SELECT * FROM	`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string INSERT = $"INSERT INTO		`{TABLE_NAME}` (`{FIELD_CITY}`) VALUE (@{FIELD_CITY});";
	private const string UPDATE = $"UPDATE			`{TABLE_NAME}` SET `{FIELD_CITY}` = @{FIELD_CITY} WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string DELETE = $"DELETE FROM		`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";

	private static LocationDTO RowMapper(MySqlDataReader mySqlDataReader)
	{
		return new LocationDTO()
		{
			Pk_location	= mySqlDataReader.GetInt32(	FIELD_PK),
			City		= mySqlDataReader.GetString(FIELD_CITY)
		};
	}

	public static List<Location> Select()
	{
		List<LocationDTO> dtos = new();

		// === COMMAND ===
		MySqlDataReader reader = InstanceDB.ExecuteReader(SLTALL);

		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperLocationWithLocationDTO.LocationsFromDTOS(dtos);
	}
	public static Location? Select(int id)
	{
		List<LocationDTO> dtos = new();

		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(SELECT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK, id);

		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperLocationWithLocationDTO.LocationsFromDTOS(dtos).FirstOrDefault();
	}
	/// <summary>
	/// Insert 
	/// </summary>
	/// <param name="location"></param>
	/// <returns>The primary key/auto increment of the new row, null if failure</returns>
	public static int? Insert(LocationNew locationNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(INSERT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_CITY, locationNew.City);

		int rowAffected = command.ExecuteNonQuery();
		if (rowAffected == 0)
			return null;

		return InstanceDB.SelectAutoIncrement(TABLE_NAME);
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="location"></param>
	/// <returns>Number of row affected</returns>
	public static bool Update(int id, LocationNew locationNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(UPDATE);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK,	id);
		command.Parameters.AddWithValue(FIELD_CITY,	locationNew.City);

		return command.ExecuteNonQuery() == 1;
	}
	public static bool Delete(int id)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(DELETE);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK, id);

		return command.ExecuteNonQuery() == 1;
	}
}
