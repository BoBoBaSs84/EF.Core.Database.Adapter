using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class MasterContextFactoryTests : InfrastructureBaseTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		MasterContextFactory contextFactory = new();

		MasterContext masterContext = contextFactory.CreateDbContext(null!);

		masterContext.Should().NotBeNull();
	}
}