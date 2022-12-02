using Microsoft.Extensions.Configuration;

namespace Database.Adapter.Infrastructure.Configurations.Infrastructure;

internal sealed class Configuration
{
	private readonly string currentDirectory = AppContext.BaseDirectory;
	private readonly string jsonFileName = @"Database.Adapter.Infrastructure.json";

	/// <summary>
	/// 
	/// </summary>
	/// <param name="contextName"></param>
	/// <returns>The connection string.</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public string GetConnectionString(string contextName)
	{
		if (string.IsNullOrWhiteSpace(contextName))
			throw new ArgumentNullException(nameof(contextName));

		IConfigurationRoot configuration = GetConfiguration();
		string server = GetServerEnviroment(configuration);
		string catalog = GetCatalog(configuration, contextName);
		string security = GetSecurity(configuration, contextName);
		string connectionString = string.Join(";", server, catalog, security);
		return connectionString;
	}

	private IConfigurationRoot GetConfiguration() =>
		new ConfigurationBuilder()
		.SetBasePath(currentDirectory)
		.AddJsonFile(jsonFileName, optional: false, reloadOnChange: true)
		.Build();

	private static string GetServerEnviroment(IConfigurationRoot configuration)
	{
		string stringToReturn = "Data Source=";
		string sqlServers = "SqlServers";
#if DEBUG
		stringToReturn += configuration.GetSection($"{sqlServers}:Development").Value;
#else
		stringToReturn += configuration.GetSection($"{sqlServers}:Production").Value;
#endif
		return stringToReturn;
	}

	private static string GetCatalog(IConfigurationRoot configuration, string contextName)
	{
		string stringToReturn = "Initial Catalog=";
		stringToReturn += configuration.GetSection($"Contexts:{contextName}:InitialCatalog").Value;
		return stringToReturn;
	}

	private static string GetSecurity(IConfigurationRoot configuration, string contextName)
	{
		string stringToReturn = "Integrated Security=True";

		// TODO: Something different than "Integrated Security=True"
		if (!string.IsNullOrWhiteSpace(configuration.GetSection("").Value))
		{
		}

		return stringToReturn;
	}

	private static IConfigurationSection GetContextSection(IConfigurationRoot configuration, string contextName) =>
		configuration.GetSection($"Contexts:{contextName}");
}
