using System.Reflection;

using BaseTests.Helpers;

using BB84.Extensions;
using BB84.Home.Application.Common;

using FluentAssertions;

namespace BB84.Home.Application.Tests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed class ArchitecturalTests : ApplicationTestBase
{
	private readonly Assembly _assembly = typeof(IApplicationAssemblyMarker).Assembly;

	[TestMethod]
	public void ServicesShouldNotBePublicAndShouldBeSealed()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase) && x.IsInterface.Equals(false)
			);

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			typeList.ForEach(t =>
			{
				t.IsSealed.Should().BeTrue();
				t.IsPublic.Should().BeFalse();
				t.IsVisible.Should().BeFalse();
			});
		});
	}

	[TestMethod]
	public void ValidatorsShouldBePublicAndShouldBeSealed()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Validator", StringComparison.OrdinalIgnoreCase) && x.IsInterface.Equals(false)
			);

		AssertionHelper.AssertInScope(() =>
		{
			typeList.Should().NotBeNullOrEmpty();
			typeList.ForEach((t) =>
			{
				t.IsSealed.Should().BeTrue();
				t.IsPublic.Should().BeTrue();
			});
		});
	}
}
