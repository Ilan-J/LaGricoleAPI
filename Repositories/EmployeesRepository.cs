using LaGricoleAPI.Models;
using LaGricoleAPI.DataBase;
using LaGricoleAPI.Utils.Mappers;
using MySqlConnector;

namespace LaGricoleAPI.Repositories;

class EmployeesRepository
{
	private const string TABLE_NAME				= "employee";
	private const string FIELD_PK				= "pk_employee";
	private const string FIELD_LASTNAME			= "lastname";
	private const string FIELD_FIRSTNAME		= "firstname";
	private const string FIELD_PHONE			= "phone";
	private const string FIELD_CELLPHONE		= "cellphone";
	private const string FIELD_EMAIL			= "email";
	private const string FIELD_FK_DEPARTMENT	= "fk_department";
	private const string FIELD_FK_LOCATION		= "fk_location";

	private const string SLTALL = $"SELECT * FROM	`{TABLE_NAME}`;";
	private const string SELECT = $"SELECT * FROM	`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";
	private const string INSERT = $"INSERT INTO		`{TABLE_NAME}`" +
		$"(`{FIELD_LASTNAME}`,	`{FIELD_FIRSTNAME}`,`{FIELD_PHONE}`,`{FIELD_CELLPHONE}`,`{FIELD_EMAIL}`,`{FIELD_FK_DEPARTMENT}`,`{FIELD_FK_LOCATION}`)" +
		"VALUE" +
		$"(@{FIELD_LASTNAME},	@{FIELD_FIRSTNAME},	@{FIELD_PHONE},	@{FIELD_CELLPHONE},	@{FIELD_EMAIL},	@{FIELD_FK_DEPARTMENT},	@{FIELD_FK_LOCATION});";
	private const string UPDATE = $"UPDATE			`{TABLE_NAME}` SET" +
		$"`{FIELD_LASTNAME}`		= @{FIELD_LASTNAME}," +
		$"`{FIELD_FIRSTNAME}`		= @{FIELD_FIRSTNAME}," +
		$"`{FIELD_PHONE}`			= @{FIELD_PHONE}," +
		$"`{FIELD_CELLPHONE}`		= @{FIELD_CELLPHONE}," +
		$"`{FIELD_EMAIL}`			= @{FIELD_EMAIL}," +
		$"`{FIELD_FK_DEPARTMENT}`	= @{FIELD_FK_DEPARTMENT}," +
		$"`{FIELD_FK_LOCATION}`		= @{FIELD_FK_LOCATION}" +
		$" WHERE `{FIELD_PK}`		= @{FIELD_PK} LIMIT 1;";
	private const string DELETE = $"DELETE FROM		`{TABLE_NAME}` WHERE `{FIELD_PK}` = @{FIELD_PK} LIMIT 1;";

	private static EmployeeDTO RowMapper(MySqlDataReader mySqlDataReader)
	{
		return new EmployeeDTO()
		{
			Pk_employee		= mySqlDataReader.GetInt32(	FIELD_PK),
			Lastname		= mySqlDataReader.GetString(FIELD_LASTNAME),
			Firstname		= mySqlDataReader.GetString(FIELD_FIRSTNAME),
			Phone			= mySqlDataReader.GetString(FIELD_PHONE),
			Cellphone		= mySqlDataReader.GetString(FIELD_CELLPHONE),
			Email			= mySqlDataReader.GetString(FIELD_EMAIL),
			Fk_department	= mySqlDataReader.GetInt32(	FIELD_FK_DEPARTMENT),
			Fk_location		= mySqlDataReader.GetInt32(	FIELD_FK_LOCATION)
		};
	}

	public static List<Employee> Select()
	{
		List<EmployeeDTO> dtos = new();

		// === COMMAND ===
		MySqlDataReader reader = InstanceDB.ExecuteReader(SLTALL);

		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperEmployeeWithEmployeeDTO.EmployeesFromDTOS(dtos);
	}
	public static Employee? Select(int id)
	{
		List<EmployeeDTO> dtos = new();

		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(SELECT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK, id);

		MySqlDataReader reader = command.ExecuteReader();
		while (reader.Read())
			dtos.Add(RowMapper(reader));

		reader.Close();
		return MapperEmployeeWithEmployeeDTO.EmployeesFromDTOS(dtos).FirstOrDefault();
	}
	public static int? Insert(EmployeeNew employeeNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(INSERT);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_LASTNAME,		employeeNew.Lastname);
		command.Parameters.AddWithValue(FIELD_FIRSTNAME,	employeeNew.Firstname);
		command.Parameters.AddWithValue(FIELD_PHONE,		employeeNew.Phone);
		command.Parameters.AddWithValue(FIELD_CELLPHONE,	employeeNew.Cellphone);
		command.Parameters.AddWithValue(FIELD_EMAIL,		employeeNew.Email);
		command.Parameters.AddWithValue(FIELD_FK_DEPARTMENT,employeeNew.DepartmentId);
		command.Parameters.AddWithValue(FIELD_FK_LOCATION,	employeeNew.LocationId);

		int rowAffected = command.ExecuteNonQuery();
		if (rowAffected == 0)
			return null;

		return InstanceDB.SelectAutoIncrement(TABLE_NAME);
	}
	public static bool Update(int id, EmployeeNew employeeNew)
	{
		// === COMMAND ===
		MySqlCommand command = InstanceDB.CreateCommand(UPDATE);
		// === PARAMETERS ===
		command.Parameters.AddWithValue(FIELD_PK,			id);
		command.Parameters.AddWithValue(FIELD_LASTNAME,		employeeNew.Lastname);
		command.Parameters.AddWithValue(FIELD_FIRSTNAME,	employeeNew.Firstname);
		command.Parameters.AddWithValue(FIELD_PHONE,		employeeNew.Phone);
		command.Parameters.AddWithValue(FIELD_CELLPHONE,	employeeNew.Cellphone);
		command.Parameters.AddWithValue(FIELD_EMAIL,		employeeNew.Email);
		command.Parameters.AddWithValue(FIELD_FK_DEPARTMENT,employeeNew.DepartmentId);
		command.Parameters.AddWithValue(FIELD_FK_LOCATION,	employeeNew.LocationId);

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