using System.Reflection;

using Application.Common;

using BaseTests.Constants;
using BaseTests.Helpers;

using FluentAssertions;

namespace ApplicationTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ArchitectureTests : ApplicationTestBase
{
	private readonly Assembly _assembly = typeof(IApplicationAssemblyMarker).Assembly;

	[TestMethod, Owner(TestConstants.Bobo)]
	[Description("The services should not be public and should be sealed.")]
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
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			}
		});
	}
}
