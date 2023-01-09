using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
public class MasterDataContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		MasterDataContext context = new(MasterDataContextFactory.DbContextOptions);
		Assert.IsNotNull(context);
	}
}