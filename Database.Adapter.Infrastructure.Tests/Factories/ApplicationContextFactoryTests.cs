using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ApplicationContextFactoryTests : InfrastructureBaseTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		ApplicationContextFactory contextFactory = new();

		ApplicationContext applicationContext = contextFactory.CreateDbContext(null!);

		applicationContext.Should().NotBeNull();
	}
}