using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
public class MasterContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		MasterContext context = new(MasterContextFactory.DbContextOptions);
		Assert.IsNotNull(context);
	}
}