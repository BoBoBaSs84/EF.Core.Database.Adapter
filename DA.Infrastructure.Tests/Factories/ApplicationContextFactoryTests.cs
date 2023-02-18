using DA.Infrastructure.Contexts;
using DA.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.Base.Tests.Constants;

namespace DA.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ApplicationContextFactoryTests : InfrastructureBaseTests
{
	[TestMethod, Owner(Bobo)]
	public void CreateDbContextTest()
	{
		ApplicationContextFactory contextFactory = new();

		ApplicationContext applicationContext = contextFactory.CreateDbContext(null!);

		applicationContext.Should().NotBeNull();
	}
}