using LaGricoleAPI.Models;
using LaGricoleAPI.DataBase;
using LaGricoleAPI.Utils.Mappers;
using MySqlConnector;

namespace LaGricoleAPI.Repositories;

class DepartementsRepository
{
	private const string TABLE_NAME	= "department";
	private const string FIELD_PK	= "pk_department";
	private const string FIELD_NAME	= "name";

	private const string SLTALL = $"SELECT * FROM	`{TABLE_NAME}`;";
	private const string SELECT = $"SELECT * FROM	`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string INSERT = $"INSERT INTO		`{TABLE_NAME}` (`{FIELD_NAME}`) VALUE (@{FIELD_NAME});";
	private const string UPDATE = $"UPDATE			`{TABLE_NAME}` SET `{FIELD_NAME}` = @{FIELD_NAME} WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string DELETE = $"DELETE FROM		`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";

	private static DepartmentDTO RowMapper(MySqlDataReader mySqlDataReader)
	{
		return new DepartmentDTO()
		{
			Pk_department	= mySqlDataReader.GetInt32(	FIELD_PK),
			Name			= mySqlDataReader.GetString(FIELD_NAME)
		};
	}

	public static List<Department> Select()
	{
		List<DepartmentDTO> dtos = new();

		// === COMMAND ===
		MySqlDataReader reader = InstanceDB.ExecuteReader(SLTALL);

		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperDepartmentWithDepartmentDTO.DepartmentsFromDTOS(dtos);
	}
	public static Department? Select(int id)
	{
		List<DepartmentDTO> dtos = new();

		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(SELECT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK, id);

		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperDepartmentWithDepartmentDTO.DepartmentsFromDTOS(dtos).FirstOrDefault();
	}
	public static int? Insert(DepartmentNew departmentNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(INSERT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_NAME, departmentNew.Name);

		int rowAffected = command.ExecuteNonQuery();
		if (rowAffected == 0)
			return null;

		return InstanceDB.SelectAutoIncrement(TABLE_NAME);
	}
	public static bool Update(int id, DepartmentNew departmentNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(UPDATE);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK,	id);
		command.Parameters.AddWithValue(FIELD_NAME,	departmentNew.Name);

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