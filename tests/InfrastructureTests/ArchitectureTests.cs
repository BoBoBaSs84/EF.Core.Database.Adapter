using System.Reflection;

using BaseTests.Helpers;

using BB84.Home.Infrastructure.Common;

using FluentAssertions;

namespace BB84.Home.Infrastructure.Tests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ArchitectureTests : InfrastructureTestBase
{
	private readonly Assembly _assembly = typeof(IInfrastructureAssemblyMarker).Assembly;

	[TestMethod]
	public void DatabaseContextsShouldNotBePublicAndShouldBeSealed()
	{
		IEnumerable<Type> contextTypes = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Context") && x.BaseType is not null
			);

		AssertionHelper.AssertInScope(() =>
		{
			contextTypes.Should().NotBeNullOrEmpty();
			foreach (Type type in contextTypes)
			{
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}

	[TestMethod]
	public void RepositoriesShouldNotBePublicAndShouldBeSealed()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Repository") && x.IsInterface.Equals(false)
			);

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			foreach (Type type in typeList)
			{
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}

	[TestMethod]
	public void ServicesShouldNotBePublicAndShouldBeSealed()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Service") && x.IsInterface.Equals(false)
			);

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			foreach (Type type in typeList)
			{
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}
}
