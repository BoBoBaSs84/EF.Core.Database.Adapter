using DA.Domain.Extensions;
using DA.Infrastructure.Exceptions;
using DA.Infrastructure.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.Infrastructure.Statics;

namespace DA.InfrastructureTests.Configurations;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ConfigurationTests : InfrastructureBaseTests
{
	private readonly Configuration configuration = new();
	private readonly string configFilePath = Path.Combine(BaseDirectory, ConfigurationFileName);
	private string jsonConfigString = string.Empty;

	[TestInitialize]
	public void TestInitialize()
	{
		configuration.Load();
		jsonConfigString = configuration.ToJsonString();
	}

	[TestCleanup]
	public void TestCleanup() => File.WriteAllText(configFilePath, jsonConfigString);

	[TestMethod, Owner(Bobo)]
	public void GetConnectionStringSuccessTest()
	{
		Configuration configuration = new();
		string connectionString = configuration.GetConnectionString("MasterContext");

		AssertInScope(() =>
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

		AssertInScope(() =>
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