using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AuthenticationContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		AuthenticationContext context = new(AuthenticationContextFactory.DbContextOptions);
		context.Should().NotBeNull();
	}
}
