using DA.BaseTests.Helpers;
using DA.Infrastructure.Configurations;
using DA.Infrastructure.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using static DA.BaseTests.Constants;
using static DA.Infrastructure.Statics;

namespace DA.InfrastructureTests.Configurations;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ConfigurationTests : InfrastructureBaseTests
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

	[TestMethod, Owner(Bobo)]
	[Description("")]
	public void GetConnectionStringSuccessTest()
	{
		Configuration configuration = new();
		string connectionString = configuration.GetConnectionString("MasterContext");

		AssertionHelper.AssertInScope(() =>
		{
			connectionString.Should().NotBeNullOrWhiteSpace();
			connectionString.Should().Contain("Server");
			connectionString.Should().Contain("Database");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void LoadSuccessTest()
	{
		Configuration configuration = new();
		configuration.Load();

		AssertionHelper.AssertInScope(() =>
		{
			configuration.SqlServers.Should().NotBeNullOrEmpty();
			configuration.Contexts.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod, Owner(Bobo)]
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

	[TestMethod, Owner(Bobo)]
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

	[TestMethod, Owner(Bobo)]
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