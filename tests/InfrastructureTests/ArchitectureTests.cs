using BaseTests.Helpers;
using FluentAssertions;
using Infrastructure.Common;
using System.Reflection;
using static BaseTests.Constants.TestConstants;

namespace InfrastructureTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ArchitectureTests : InfrastructureBaseTests
{
	private readonly Assembly _assembly = typeof(IInfrastructureAssemblyMarker).Assembly;

	[TestMethod, Owner(Bobo)]
	public void ContextMustBeSealedTest()
	{
		IEnumerable<Type> contextTypes = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Context") && x.BaseType is not null
			&& (x.BaseType.Name.Contains("DbContext") || x.BaseType.Name.Contains("IdentityDbContext"))
			);

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

	[TestMethod, Owner(Bobo)]
	public void RepositoriesShouldNotBePublicAndShouldBeSealedTest()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Repository") && x.IsInterface.Equals(false)
			);

		typeList.Should().NotBeNullOrEmpty();

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			foreach (Type type in typeList)
			{
				TestContext.WriteLine($"Testing: {type.Name}");
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}

	[TestMethod, Owner(Bobo)]
	public void ServicesShouldNotBePublicAndShouldBeSealedTest()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Service") && x.IsInterface.Equals(false)
			);

		typeList.Should().NotBeNullOrEmpty();

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			foreach (Type type in typeList)
			{
				TestContext.WriteLine($"Testing: {type.Name}");
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}
}
