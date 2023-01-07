using Database.Adapter.Infrastructure.Exceptions;
using System.Text.Json;
using static Database.Adapter.Infrastructure.Properties.Resources;
using static Database.Adapter.Infrastructure.Statics;

namespace Database.Adapter.Infrastructure.Configurations;

public sealed class Configuration
{
	private bool loaded;

	public List<SqlServer> SqlServers { get; set; }
	public List<Context> Contexts { get; set; }

	public class Context
	{
		public string Name { get; set; }
		public string Database { get; set; }
		public bool IntegratedSecurity { get; set; }
		public bool MultipleActiveResultSets { get; set; }
		public bool PersistSecurityInfo { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public class SqlServer
	{
		public string Name { get; set; }
		public string Environment { get; set; }
		public string Server { get; set; }
	}

	/// <summary>
	/// Should return the proper connection string for the provided database context.
	/// </summary>
	/// <param name="contextName">The name of the database context.</param>
	/// <returns>The connection string.</returns>
	/// <exception cref="ConfigurationException"></exception>
	public string GetConnectionString(string contextName)
	{
		try
		{
			if (!loaded)
				Load();

			string server = GetServer();
			string database = GetDatabase(contextName);
			string security = GetSecurity(contextName);
			string appName = GetApplicatioName();
			string connectionString = string.Join(";", server, database, security, appName);
			return connectionString;
		}
		catch(Exception ex)
		{
			string message = string.Format(CurrentCulture, Exception_Configuration_GetConnectionString, contextName);
			throw new ConfigurationException(message, ex);
		}
	}

	private static string GetApplicatioName() =>
		$"Application Name={AppDomainName}@{MachineName}\\{UserName}";

	private string GetDatabase(string contextName)
	{
		Context context = GetContext(contextName);
		return $"{nameof(context.Database)}={context.Database}";
	}

	private string GetSecurity(string contextName)
	{
		Context context = GetContext(contextName);
		string stringToReturn = "Integrated Security=True";

		return context.IntegratedSecurity
			? stringToReturn
			: $"Integrated Security=False;User Id={context.UserName};Password={context.Password}";
	}
	private string GetServer(string environment = "Development")
	{
		SqlServer sqlServer = GetSqlServer(environment);
		return $"{nameof(sqlServer.Server)}={sqlServer.Server}";
	}

	private Context GetContext(string contextName)
	{
		try
		{
			return Contexts.Single(x => x.Name.Equals(contextName, StringComparison.Ordinal));
		}
		catch(Exception ex)
		{
			string message = string.Format(Culture, Exception_Configuration_GetContextFailed);
			throw new ConfigurationException(message, ex);
		}
	}

	private SqlServer GetSqlServer(string environment)
	{
		try
		{
			return SqlServers.Single(x => x.Environment.Equals(environment, StringComparison.Ordinal));
		}
		catch(Exception ex)
		{
			string message = string.Format(Culture, Exception_Configuration_GetGetSqlServerFailed);
			throw new ConfigurationException(message, ex);
		}
	}

	private void Load()
	{
		try
		{
			if (loaded)
			{
				string message = string.Format(Culture, Exception_Configuration_Load_AlreadyLoaded);
				throw new ConfigurationException(message);
			}

			string jsonString = File.ReadAllText(Path.Combine(BaseDirectory, ConfigurationFileName));

			if (string.IsNullOrWhiteSpace(jsonString))
			{
				string message = string.Format(Culture, Exception_Configuration_Load_FileFailed, jsonString);
				throw new ConfigurationException(message);
			}

			Configuration? configuration = JsonSerializer.Deserialize<Configuration>(jsonString);

			if (configuration is null)
			{
				string message = string.Format(Culture, Exception_Configuration_Load_FileReadFailed);
				throw new ConfigurationException(message);
			}

			SqlServers = configuration.SqlServers;
			Contexts = configuration.Contexts;
			loaded = true;
		}
		catch (Exception ex)
		{
			string message = string.Format(Culture, Exception_Configuration_Load_Failed);
			throw new ConfigurationException(message, ex);
		}
	}
}
