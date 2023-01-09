using Database.Adapter.Infrastructure.Exceptions;
using System.Text.Json;
using static Database.Adapter.Infrastructure.Properties.Resources;
using static Database.Adapter.Infrastructure.Statics;

namespace Database.Adapter.Infrastructure.Configurations;

/// <summary>
/// The configuration class.
/// </summary>
/// <remarks>
/// This strongly typed class is for the context and server configuration.
/// </remarks>
public sealed class Configuration
{
	private bool loaded;

	/// <summary>The <see cref="SqlServers"/> property.</summary>
	public List<SqlServer> SqlServers { get; set; } = new();
	/// <summary>The <see cref="Contexts"/> property.</summary>
	public List<Context> Contexts { get; set; } = new();
	/// <summary>
	/// The context/database class.
	/// </summary>
	public class Context
	{
		/// <summary>The <see cref="Name"/> property.</summary>
		public string Name { get; set; } = default!;
		/// <summary>The <see cref="Database"/> property.</summary>
		public string Database { get; set; } = default!;
		/// <summary>The <see cref="IntegratedSecurity"/> property.</summary>
		public bool IntegratedSecurity { get; set; } = default!;
		/// <summary>The <see cref="MultipleActiveResultSets"/> property.</summary>
		public bool MultipleActiveResultSets { get; set; } = default!;
		/// <summary>The <see cref="PersistSecurityInfo"/> property.</summary>
		public bool PersistSecurityInfo { get; set; } = default!;
		/// <summary>The <see cref="UserName"/> property.</summary>
		public string? UserName { get; set; } = default!;
		/// <summary>The <see cref="Password"/> property.</summary>
		public string? Password { get; set; } = default!;
	}
	/// <summary>
	/// The sql server class.
	/// </summary>
	public class SqlServer
	{
		/// <summary>The <see cref="Name"/> property.</summary>
		public string Name { get; set; } = default!;
		/// <summary>The <see cref="Environment"/> property.</summary>
		public string Environment { get; set; } = default!;
		/// <summary>The <see cref="Server"/> property.</summary>
		public string Server { get; set; } = default!;
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
