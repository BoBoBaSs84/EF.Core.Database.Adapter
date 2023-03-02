using BaseTests.Helpers;
using FluentAssertions;
using Infrastructure.Common;
using System.Reflection;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ApplicationContextTests : InfrastructureBaseTests
{
	[TestMethod, Owner(Bobo)]
	public void MustBeSealedTest()
	{
		IEnumerable<Type> contextTypes = GetContextTypes();

		AssertionHelper.AssertInScope(() =>
		{
			contextTypes.Should().NotBeNullOrEmpty();
			foreach (Type type in contextTypes)
			{
				TestContext.WriteLine($"Testing: {type.Name}");
				type.IsSealed.Should().BeTrue();
			}
		});
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