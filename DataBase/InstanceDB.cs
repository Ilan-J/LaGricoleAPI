using MySqlConnector;

namespace LaGricoleAPI.DataBase;

static class InstanceDB
{
	public static MySqlConnection Connection { get; }

	static InstanceDB()
	{
		MySqlConnectionStringBuilder builder = new()
		{
			Server		= ConfigDB.Host,
			Port		= ConfigDB.Port,
			UserID		= ConfigDB.UserId,
			Password	= ConfigDB.Password,
			Database	= ConfigDB.Database
		};
		Connection = new MySqlConnection(builder.ConnectionString);

		if (!Open()) Console.Error.WriteLine("Failed Initial DB Connection!");
	}

	/// <summary>
	/// Opens connection to database
	/// </summary>
	/// <returns>true if success, otherwise false</returns>
	public static bool Open()
	{
		try
		{
			Connection.Open();
			return true;
		}
		catch (MySqlException ex)
		{
			Console.WriteLine(ex.Message);
			return false;
		}
	}
	/// <summary>
	/// Closes connection to database
	/// </summary>
	public static void Close()
	{
		Connection.Close();
	}




	public static MySqlCommand CreateCommand(string commandText)
	{
		MySqlCommand sqlCommand = Connection.CreateCommand();
		sqlCommand.CommandText = commandText;

		return sqlCommand;
	}

	public static MySqlDataReader ExecuteReader(string sql)
	{
		MySqlCommand sqlCommand = Connection.CreateCommand();
		sqlCommand.CommandText = sql;

		return sqlCommand.ExecuteReader();
	}

	public static int SelectAutoIncrement(string table)
	{
		const string sql = $"SELECT `AUTO_INCREMENT` FROM `INFORMATION_SCHEMA`.`TABLES` WHERE `TABLE_SCHEMA` = @database AND `TABLE_NAME` = @table;";
		MySqlCommand sqlCommand = Connection.CreateCommand();
		sqlCommand.CommandText = sql;

		// === PARAMETERS ===
		sqlCommand.Parameters.AddWithValue("database",	ConfigDB.Database);
		sqlCommand.Parameters.AddWithValue("table",		table);

		MySqlDataReader mySqlDataReader = sqlCommand.ExecuteReader();
		if (mySqlDataReader.Read())
		{
			int autoIncrement = mySqlDataReader.GetInt32("AUTO_INCREMENT") -1;

			mySqlDataReader.Close();
			return autoIncrement;
		}
		mySqlDataReader.Close();
		throw new ArgumentException($"Table '{table}' doesn't contain any AUTO_INCREMENT column");
	}
}