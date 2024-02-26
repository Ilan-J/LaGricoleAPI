using LaGricoleAPI.Models;
using LaGricoleAPI.DataBase;
using LaGricoleAPI.Utils.Mappers;
using MySqlConnector;

namespace LaGricoleAPI.Repositories;

class AdminsRepository
{
	private const string TABLE_NAME			= "employee";
	private const string FIELD_PK			= "pk_employee";
	private const string FIELD_EMAIL		= "email";
	private const string FIELD_PASSWORD		= "password";

	private const string SLTALL = $"SELECT {FIELD_PK}	FROM	`{TABLE_NAME}` WHERE `{FIELD_PASSWORD}` IS NOT NULL;";
	private const string SELECT = $"SELECT *			FROM	`{TABLE_NAME}` WHERE `{FIELD_EMAIL}` = @{FIELD_EMAIL} AND `{FIELD_PASSWORD}` = @{FIELD_PASSWORD} LIMIT 1;";
	private const string INSERT = $"UPDATE				`{TABLE_NAME}` SET `{FIELD_PASSWORD}` = @{FIELD_PASSWORD}	WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string UPDATE = $"UPDATE				`{TABLE_NAME}` SET `{FIELD_PASSWORD}` = @{FIELD_PASSWORD}	WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string DELETE = $"UPDATE				`{TABLE_NAME}` SET `{FIELD_PASSWORD}` = NULL				WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";

	private static AdminDTO RowMapper(MySqlDataReader mySqlDataReader)
	{
		return new AdminDTO()
		{
			Pk_employee	= mySqlDataReader.GetInt32(	FIELD_PK),
			Email		= mySqlDataReader.GetString(FIELD_EMAIL),
			Password	= mySqlDataReader.GetString(FIELD_PASSWORD),
		};
	}

	public static List<int> Select()
	{
		List<int> pkList = new();

		// === COMMAND ===
		MySqlDataReader reader = InstanceDB.ExecuteReader(SLTALL);

		while (reader.Read())
			pkList.Add(RowMapper(reader).Pk_employee);

		reader.Close();
		return pkList;
	}
	public static Admin? Select(string email, string password)
	{
		List<AdminDTO> dtos = new();

		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(SELECT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_EMAIL,	email);
		command.Parameters.AddWithValue(FIELD_PASSWORD, password);

		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperAdminWithAdminDTO.AdminsFromDTOS(dtos).FirstOrDefault();
	}
	public static bool Insert(AdminNew adminNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(INSERT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK,		adminNew.Id);
		command.Parameters.AddWithValue(FIELD_PASSWORD,	adminNew.Password);

		return command.ExecuteNonQuery() == 1;
	}
	public static bool Update(AdminNew adminNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(UPDATE);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK,		adminNew.Id);
		command.Parameters.AddWithValue(FIELD_PASSWORD,	adminNew.Password);

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
