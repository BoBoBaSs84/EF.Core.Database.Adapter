using Database.Adapter.Infrastructure.Configurations;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Database.Adapter.Infrastructure.Tests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class InfrastructureBaseTests
{
	private readonly Assembly _assembly = typeof(Statics).Assembly;

	[TestMethod]
	public void DatabaseContextHaveConfigurationTest()
	{
		ICollection<Type> contextTypes = GetContextTypes();

		foreach (Type type in contextTypes)
		{
			Configuration configuration = new();
			string connectionString = configuration.GetConnectionString(type.Name);
			connectionString.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod]
	public void DatabaseContextMustBeSealed()
	{
		ICollection<Type> contextTypes = GetContextTypes();
		foreach(Type type in contextTypes)
			type.IsSealed.Should().BeTrue();
	}

	private ICollection<Type> GetContextTypes()
	{
		List<Type> types = new();
		foreach (Type type in _assembly.GetTypes().Where(x => x.BaseType is not null && (x.BaseType.Equals(typeof(DbContext)) || x.BaseType.Equals(typeof(IdentityDbContext)))))
			types.Add(type);
		return types;
	}
}
