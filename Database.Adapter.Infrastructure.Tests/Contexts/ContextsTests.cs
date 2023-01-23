using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Infrastructure.Common;
using Database.Adapter.Infrastructure.Configurations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Database.Adapter.Infrastructure.Tests.Contexts;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ContextsTests : InfrastructureBaseTests
{
	[TestMethod]
	public void DatabaseContextHaveConfigurationTest()
	{
		ICollection<Type> contextTypes = GetContextTypes();
		contextTypes.Should().NotBeNullOrEmpty();

		foreach (Type type in contextTypes)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			Configuration configuration = new();
			string connectionString = configuration.GetConnectionString(type.Name);
			connectionString.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod]
	public void DatabaseContextMustBeSealed()
	{
		ICollection<Type> contextTypes = GetContextTypes();
		contextTypes.Should().NotBeNullOrEmpty();

		foreach (Type type in contextTypes)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			type.IsSealed.Should().BeTrue();
		}
	}

	private static ICollection<Type> GetContextTypes()
	{
		Assembly assembly = typeof(IAssemblyMarker).Assembly;
		return TypeHelper.GetAssemblyTypes(
			assembly,
			x => x.Name.EndsWith("Context")
			&& x.BaseType is not null
			&& x.BaseType.BaseType is not null
			&& x.BaseType.BaseType.Equals(typeof(DbContext)));
	}
}