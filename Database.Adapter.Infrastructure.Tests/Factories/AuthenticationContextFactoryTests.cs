using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
public class AuthenticationContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		AuthenticationContext context = new(AuthenticationContextFactory.DbContextOptions);
		Assert.IsNotNull(context);
	}
}