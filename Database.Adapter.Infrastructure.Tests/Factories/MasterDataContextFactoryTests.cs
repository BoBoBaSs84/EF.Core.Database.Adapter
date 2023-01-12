using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class MasterDataContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		MasterDataContext context = new(MasterDataContextFactory.DbContextOptions);
		context.Should().NotBeNull();
	}
}