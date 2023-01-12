using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class MasterContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		MasterContext context = new(MasterContextFactory.DbContextOptions);
		context.Should().NotBeNull();
	}
}