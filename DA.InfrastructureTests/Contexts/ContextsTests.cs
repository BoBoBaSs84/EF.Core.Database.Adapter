﻿using DA.BaseTests.Helpers;
using DA.Infrastructure.Common;
using DA.Infrastructure.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static DA.BaseTests.Constants;

namespace DA.InfrastructureTests.Contexts;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ContextsTests : InfrastructureBaseTests
{
	[TestMethod, Owner(Bobo)]
	public void DatabaseContextHaveConfigurationTest()
	{
		IEnumerable<Type> contextTypes = GetContextTypes();
		contextTypes.Should().NotBeNullOrEmpty();

		foreach (Type type in contextTypes)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			Configuration configuration = new();
			string connectionString = configuration.GetConnectionString(type.Name);
			connectionString.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod, Owner(Bobo)]
	public void DatabaseContextMustBeSealed()
	{
		IEnumerable<Type> contextTypes = GetContextTypes();
		contextTypes.Should().NotBeNullOrEmpty();

		foreach (Type type in contextTypes)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			type.IsSealed.Should().BeTrue();
		}
	}

	private static IEnumerable<Type> GetContextTypes()
	{
		Assembly assembly = typeof(IInfrastructureAssemblyMarker).Assembly;
		return TypeHelper.GetAssemblyTypes(
			assembly: assembly,
			expression: x => x.Name.EndsWith("Context") && x.BaseType is not null && (x.BaseType.Name.Contains("DbContext") || x.BaseType.Name.Contains("IdentityDbContext"))
			);
	}
}