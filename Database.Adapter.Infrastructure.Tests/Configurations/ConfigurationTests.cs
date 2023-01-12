using Database.Adapter.Infrastructure.Configurations;
using Database.Adapter.Infrastructure.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using static Database.Adapter.Infrastructure.Statics;

namespace Database.Adapter.Infrastructure.Tests.Configurations;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ConfigurationTests
{
	private readonly Configuration configuration = new();
	private readonly string configFilePath = Path.Combine(BaseDirectory, ConfigurationFileName);
	private readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = false };
	private string configJson = string.Empty;

	[TestInitialize]
	public void TestInitialize()
	{
		configuration.Load();
		configJson = JsonSerializer.Serialize(configuration, jsonSerializerOptions);
	}

	[TestCleanup]
	public void TestCleanup() => File.WriteAllText(configFilePath, configJson);

	[TestMethod]
	public void GetConnectionStringSuccessTest()
	{
		Configuration configuration = new();
		string connectionString = configuration.GetConnectionString("MasterContext");
		connectionString.Should().NotBeNullOrWhiteSpace();
	}

	[TestMethod]
	public void LoadSuccessTest()
	{
		Configuration configuration = new();
		configuration.Load();
		configuration.SqlServers.Should().NotBeNullOrEmpty();
		configuration.Contexts.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public void LoadExceptionAlreadyLoadedTest()
	{
		Configuration configuration = new();
		try
		{
			configuration.Load();
			configuration.Load();
		}
		catch (Exception ex)
		{
			ex.Should().BeOfType<ConfigurationException>();
		}
	}

	[TestMethod]
	public void LoadExceptionFileFailedTest()
	{
		Configuration configuration = new();

		if (File.Exists(configFilePath))
			File.WriteAllText(configFilePath, "");

		try
		{
			configuration.Load();
		}
		catch (Exception ex)
		{
			ex.Should().BeOfType<ConfigurationException>();
		}
	}

	[TestMethod]
	public void LoadExceptionFailedTest()
	{
		Configuration configuration = new();

		if (File.Exists(configFilePath))
			File.Delete(configFilePath);

		try
		{
			configuration.Load();
		}
		catch (Exception ex)
		{
			ex.Should().BeOfType<ConfigurationException>();
		}
	}
}